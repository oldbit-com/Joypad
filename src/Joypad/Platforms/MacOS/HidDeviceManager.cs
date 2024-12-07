using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static OldBit.Joypad.Platforms.MacOS.Interop.CoreFoundation;
using static OldBit.Joypad.Platforms.MacOS.Interop.IOKit;

namespace OldBit.Joypad.Platforms.MacOS;

internal class ControllerEventArgs(HidController controller) : EventArgs
{
    public HidController Controller { get; } = controller;
}

[SupportedOSPlatform("macos")]
internal class HidDeviceManager : IDeviceManager
{
    private IntPtr _manager = IntPtr.Zero;
    private IntPtr _runLoop = IntPtr.Zero;
    private GCHandle _gch;
    private readonly Thread _runLoopThread;

    internal event EventHandler<ControllerEventArgs>? ControllerAdded;
    internal event EventHandler<ControllerEventArgs>? ControllerRemoved;
    internal event EventHandler<ErrorEventArgs>? ErrorOccurred;

    internal HidDeviceManager()
    {
        _gch = GCHandle.Alloc(this);

        _runLoopThread = new Thread(RunLoopRunThread)
        {
            IsBackground = true,
            Name = "HID Device Listener"
        };
    }

    public void StartListener() => _runLoopThread.Start();

    public void StopListener()
    {
        if (_runLoop != IntPtr.Zero)
        {
            IOHIDManagerUnscheduleFromRunLoop(_manager, _runLoop, kCFRunLoopDefaultMode);
            CFRunLoopStop(_runLoop);

            _runLoop = IntPtr.Zero;
        }

        if (_manager != IntPtr.Zero)
        {
            var result = IOHIDManagerClose(_manager);
            ThrowIfError(result, "Failed to close HID manager");

            _manager = IntPtr.Zero;
        }

        _runLoopThread.Join();
    }

    private void RunLoopRunThread()
    {
        try
        {
            RunLoopRun();
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke(this, new ErrorEventArgs(ex));
        }
    }

    private void RunLoopRun()
    {
        _manager = IOHIDManagerCreate(IntPtr.Zero);

        var deviceFilter = GetUsageFilter();
        IOHIDManagerSetDeviceMatchingMultiple(_manager, deviceFilter);

        var context = GCHandle.ToIntPtr(_gch);
        unsafe
        {
            IOHIDManagerRegisterDeviceMatchingCallback(_manager, &DeviceAddedCallback, context);
            IOHIDManagerRegisterDeviceRemovalCallback(_manager, &DeviceRemovedCallback, context);
        }

        var result = IOHIDManagerOpen(_manager);
        ThrowIfError(result, "Failed to open HID manager");

        _runLoop = CFRunLoopGetCurrent();
        IOHIDManagerScheduleWithRunLoop(_manager, _runLoop, kCFRunLoopDefaultMode);

        CFRunLoopRun();
    }

    private static void ThrowIfError(int result, string message)
    {
        if (result != kIOReturnSuccess)
        {
            throw new JoypadException(message, result);
        }
    }

    [UnmanagedCallersOnly]
    private static void DeviceAddedCallback(IntPtr context, int result, IntPtr sender, IntPtr device)
    {
        if (!TryGetDeviceManagerFromContext(context, out var deviceManager))
        {
            return;
        }

        var controller = new HidController(device);

        controller.ProcessElements();
        controller.Initialize();

        deviceManager.ControllerAdded?.Invoke(deviceManager, new ControllerEventArgs(controller));
    }

    [UnmanagedCallersOnly]
    private static void DeviceRemovedCallback(IntPtr context, int result, IntPtr sender, IntPtr device)
    {
        if (!TryGetDeviceManagerFromContext(context, out var deviceManager))
        {
            return;
        }

        var controller = new HidController(device);

        deviceManager.ControllerRemoved?.Invoke(deviceManager, new ControllerEventArgs(controller));
    }

    private static bool TryGetDeviceManagerFromContext(IntPtr context, [NotNullWhen(true)] out HidDeviceManager? deviceManager)
    {
        var gch = GCHandle.FromIntPtr(context);

        if (gch.Target is HidDeviceManager manager)
        {
            deviceManager = manager;
            return true;
        }

        deviceManager = null;
        return false;
    }

    private static IntPtr GetUsageFilter()
    {
        var filters = CFArrayCreateMutable();

        AddUsageFilter(filters, kHIDPage_GenericDesktop, kHIDUsage_GD_GamePad);
        AddUsageFilter(filters, kHIDPage_GenericDesktop, kHIDUsage_GD_Joystick);
        AddUsageFilter(filters, kHIDPage_GenericDesktop, kHIDUsage_GD_MultiAxisController);

        return filters;
    }

    private static void AddUsageFilter(IntPtr filters, int usagePage, int usage)
    {
        var filter = CFDictionaryCreateMutable();

        CFDictionaryAddValue(filter, kIOHIDDeviceUsagePageKey, usagePage);
        CFDictionaryAddValue(filter, kIOHIDDeviceUsageKey, usage);

        CFArrayAppendValue(filters, filter);
    }

    public void Dispose()
    {
        if (_gch.IsAllocated)
        {
            _gch.Free();
        }

        GC.SuppressFinalize(this);
    }
}
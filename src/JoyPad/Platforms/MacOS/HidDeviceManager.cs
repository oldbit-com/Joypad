using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static OldBit.JoyPad.Platforms.MacOS.Interop.CoreFoundation;
using static OldBit.JoyPad.Platforms.MacOS.Interop.IOKit;

namespace OldBit.JoyPad.Platforms.MacOS;

internal class ControllerEventArgs(HidController controller) : EventArgs
{
    public HidController Controller { get; } = controller;
}

[SupportedOSPlatform("macos")]
internal class HidDeviceManager : IDisposable, IDeviceManager
{
    private IntPtr _manager;
    private IntPtr _runLoop;
    private GCHandle _gch;

    internal event EventHandler<ControllerEventArgs>? ControllerAdded;
    internal event EventHandler<ControllerEventArgs>? ControllerRemoved;

    internal HidDeviceManager()
    {
        _gch = GCHandle.Alloc(this);
        _manager = IOHIDManagerCreate(IntPtr.Zero);
        _runLoop = CFRunLoopGetCurrent();
    }

    public void StartListener()
    {
        IOHIDManagerScheduleWithRunLoop(_manager, _runLoop, kCFRunLoopDefaultMode);

        var deviceFilter = GetUsageFilter();
        IOHIDManagerSetDeviceMatchingMultiple(_manager, deviceFilter);

        var result = IOHIDManagerOpen(_manager);
        if (result != kIOReturnSuccess)
        {
            // TODO: Throw
        }

        var context = GCHandle.ToIntPtr(_gch);

        unsafe
        {
            IOHIDManagerRegisterDeviceMatchingCallback(_manager, &DeviceAddedCallback, context);
            IOHIDManagerRegisterDeviceRemovalCallback(_manager, &DeviceRemovedCallback, context);
        }

        CFRunLoopRun();
    }

    // public void Stop()
    // {
    //     IOHIDManagerUnscheduleFromRunLoop(_manager, _runLoop, kCFRunLoopDefaultMode);
    // }

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

    [UnmanagedCallersOnly]
    private static void DeviceAddedCallback(IntPtr context, int result, IntPtr sender, IntPtr device)
    {
        if (!TryGetDeviceManagerFromContext(context, out var deviceManager))
        {
            return;
        }

        var controller = new HidController(device);
        controller.ProcessElements();

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

    private void ReleaseUnmanagedResources()
    {
        if (_manager == IntPtr.Zero)
        {
            return;
        }

        IOHIDManagerUnscheduleFromRunLoop(_manager, _runLoop, kCFRunLoopDefaultMode);
        _ =IOHIDManagerClose(_manager);

        _manager = IntPtr.Zero;
        _runLoop = IntPtr.Zero;
    }

    public void Dispose()
    {
        if (_gch.IsAllocated)
        {
            _gch.Free();
        }

        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~HidDeviceManager()
    {
        ReleaseUnmanagedResources();
    }
}
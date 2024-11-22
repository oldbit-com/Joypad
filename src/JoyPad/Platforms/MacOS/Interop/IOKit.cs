using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace OldBit.JoyPad.Platforms.MacOS.Interop;

[SupportedOSPlatform("macos")]
internal static unsafe partial class IOKit
{
    private const string IOKitLibrary = "/System/Library/Frameworks/IOKit.framework/IOKit";

    [LibraryImport(IOKitLibrary)]
    internal static partial IntPtr IOHIDManagerCreate(IntPtr allocator, int options = kIOHIDOptionsTypeNone);

    [LibraryImport(IOKitLibrary)]
    internal static partial int IOHIDManagerOpen(IntPtr manager, int options = kIOHIDOptionsTypeNone);

    [LibraryImport(IOKitLibrary)]
    internal static partial int IOHIDManagerClose(IntPtr manager, int options = kIOHIDOptionsTypeNone);

    [LibraryImport(IOKitLibrary)]
    internal static partial void IOHIDManagerSetDeviceMatchingMultiple(IntPtr manager, IntPtr multiple);

    [LibraryImport(IOKitLibrary)]
    internal static partial void IOHIDManagerScheduleWithRunLoop(IntPtr manager, IntPtr runLoop, IntPtr runLoopMode);

    [LibraryImport(IOKitLibrary)]
    internal static partial void IOHIDManagerUnscheduleFromRunLoop(IntPtr manager, IntPtr runLoop, IntPtr runLoopMode);

    [LibraryImport(IOKitLibrary)]
    internal static unsafe partial void IOHIDManagerRegisterDeviceMatchingCallback(IntPtr manager, delegate* unmanaged<IntPtr, int, IntPtr, IntPtr, void> callback, IntPtr context);

    [LibraryImport(IOKitLibrary)]
    internal static unsafe partial void IOHIDManagerRegisterDeviceRemovalCallback(IntPtr manager, delegate* unmanaged<IntPtr, int, IntPtr, IntPtr, void> callback, IntPtr context);

    [LibraryImport(IOKitLibrary)]
    internal static partial IntPtr IOHIDDeviceGetProperty(IntPtr device, IntPtr key);

    [LibraryImport(IOKitLibrary)]
    internal static partial IntPtr IOHIDDeviceCopyMatchingElements(IntPtr device, IntPtr matching, int options = kIOHIDOptionsTypeNone);

    [LibraryImport(IOKitLibrary)]
    internal static partial IOHIDElementType IOHIDElementGetType(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial uint IOHIDElementGetTypeID();

    [LibraryImport(IOKitLibrary)]
    internal static partial uint IOHIDElementGetUsagePage(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial uint IOHIDElementGetUsage(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial IntPtr IOHIDElementGetChildren(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial uint IOHIDElementGetCookie(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial int  IOHIDElementGetLogicalMin(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial int  IOHIDElementGetLogicalMax(IntPtr element);

    [LibraryImport(IOKitLibrary)]
    internal static partial int IOHIDValueGetIntegerValue(IntPtr value);

    [LibraryImport(IOKitLibrary)]
    internal static partial int IOHIDDeviceGetValue(IntPtr device, IntPtr element, IntPtr *value);

    [LibraryImport(IOKitLibrary)]
    internal static partial IntPtr IOHIDValueCreateWithIntegerValue(IntPtr allocator, IntPtr element, ulong timestamp, int value);
}
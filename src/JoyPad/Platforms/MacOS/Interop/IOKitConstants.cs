using System.Diagnostics.CodeAnalysis;

namespace OldBit.JoyPad.Platforms.MacOS.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
internal partial class IOKit
{
    private const int kIOHIDOptionsTypeNone = 0;
    internal const int kIOReturnSuccess = 0;

    internal const int kHIDPage_GenericDesktop = 0x01;
    internal const int kHIDPage_Button = 0x09;

    internal const int kHIDUsage_GD_Joystick = 0x04;
    internal const int kHIDUsage_GD_GamePad = 0x05;
    internal const int kHIDUsage_GD_MultiAxisController = 0x08;
    internal const int kHIDUsage_GD_X = 0x30;
    internal const int kHIDUsage_GD_Y = 0x31;
    internal const int kHIDUsage_GD_Z = 0x32;
    internal const int kHIDUsage_GD_Rx = 0x33;
    internal const int kHIDUsage_GD_Ry = 0x34;
    internal const int kHIDUsage_GD_Rz = 0x35;
    internal const int kHIDUsage_GD_Hatswitch = 0x39;

    private static IntPtr _kCFRunLoopDefaultMode = IntPtr.Zero;
    private static IntPtr _kIOHIDDeviceUsagePageKey = IntPtr.Zero;
    private static IntPtr _kIOHIDDeviceUsageKey = IntPtr.Zero;
    private static IntPtr _kIOHIDProductKey = IntPtr.Zero;
    private static IntPtr _kIOHIDProductIDKey = IntPtr.Zero;
    private static IntPtr _kIOHIDVendorIDKey = IntPtr.Zero;
    private static IntPtr _kIOHIDVersionNumberKey = IntPtr.Zero;
    private static IntPtr _kIOHIDTransportKey = IntPtr.Zero;

    internal static IntPtr kCFRunLoopDefaultMode => CreateOrGet("kCFRunLoopDefaultMode", ref _kCFRunLoopDefaultMode);
    internal static IntPtr kIOHIDDeviceUsagePageKey => CreateOrGet("DeviceUsagePage", ref _kIOHIDDeviceUsagePageKey);
    internal static IntPtr kIOHIDDeviceUsageKey => CreateOrGet("DeviceUsage", ref _kIOHIDDeviceUsageKey);
    internal static IntPtr kIOHIDProductKey => CreateOrGet("Product", ref _kIOHIDProductKey);
    internal static IntPtr kIOHIDProductIDKey => CreateOrGet("ProductID", ref _kIOHIDProductIDKey);
    internal static IntPtr kIOHIDVendorIDKey => CreateOrGet("VendorID", ref _kIOHIDVendorIDKey);
    internal static IntPtr kIOHIDVersionNumberKey => CreateOrGet("VersionNumber", ref _kIOHIDVersionNumberKey);
    internal static IntPtr kIOHIDTransportKey => CreateOrGet("Transport", ref _kIOHIDTransportKey);
}
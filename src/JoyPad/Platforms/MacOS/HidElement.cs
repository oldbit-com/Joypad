using System.Runtime.Versioning;
using OldBit.JoyPad.Controls;
using static OldBit.JoyPad.Platforms.MacOS.Interop.CoreFoundation;
using static OldBit.JoyPad.Platforms.MacOS.Interop.IOKit;

namespace OldBit.JoyPad.Platforms.MacOS;

[SupportedOSPlatform("macos")]
internal class HidElement : Control, IDisposable
{
    private readonly IntPtr _device;

    internal IntPtr Element { get; }

    private IntPtr _valueRef;
    private int Min { get; }
    private int Max { get; }
    private uint Usage { get; }

    private HidElement(
        IntPtr device,
        ControlType controlType,
        IntPtr element,
        uint usage) : base(controlType)
    {
        _device = device;
        _valueRef = IOHIDValueCreateWithIntegerValue(IntPtr.Zero, element, 0, 0);

        Element = element;
        Usage = usage;

        Id = (int)IOHIDElementGetCookie(element);;
        Min = IOHIDElementGetLogicalMin(element);
        Max = IOHIDElementGetLogicalMax(element);

        Name = GetName();
    }

    internal static HidElement CreateButton(IntPtr device, IntPtr element, uint usage) =>
        new(device, ControlType.Button, element, usage);

    internal static HidElement CreateHat(IntPtr device, IntPtr element, uint usage) =>
        new(device, ControlType.DirectionalPad, element, usage);

    internal static HidElement CreateAnalog(IntPtr device, IntPtr element, uint usage) =>
        new(device, ControlType.ThumbStick, element, usage);

    internal int? GetValue()
    {
        int result;

        unsafe
        {
            fixed (IntPtr* valueRef = &_valueRef)
            {
                result = IOHIDDeviceGetValue(_device, Element, valueRef);
            }
        }

        if (result == kIOReturnSuccess)
        {
            return IOHIDValueGetIntegerValue(_valueRef);
        }

        return null;
    }

    private string GetUsageName() => Usage switch
    {
        kHIDUsage_GD_X => "X",
        kHIDUsage_GD_Y => "Y",
        kHIDUsage_GD_Z => "Z",
        kHIDUsage_GD_Rx => "Rx",
        kHIDUsage_GD_Ry => "Ry",
        kHIDUsage_GD_Rz => "Rz",
        _ => Usage.ToString()
    };

    private string GetName() => ControlType switch
    {
        ControlType.Button => $"Button {GetUsageName()}",
        ControlType.ThumbStick => $"Thumb Stick {GetUsageName()}",
        ControlType.DirectionalPad => "Directional Pad",
        _ => string.Empty
    };

    private void ReleaseUnmanagedResources()
    {
        if (_valueRef == IntPtr.Zero)
        {
            return;
        }

        CFRelease(_valueRef);
        _valueRef = IntPtr.Zero;
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~HidElement()
    {
        ReleaseUnmanagedResources();
    }

    public override string ToString() => Name;
}
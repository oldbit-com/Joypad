using System.Runtime.Versioning;
using static OldBit.JoyPad.Platforms.MacOS.Interop.IOKit;

namespace OldBit.JoyPad.Platforms.MacOS;

[SupportedOSPlatform("macos")]
internal class HidElement : Control
{
    internal IntPtr Element { get; }
    internal uint Cookie { get; }
    internal int Min { get; }
    internal int Max { get; }
    internal uint Usage { get; }

    private HidElement(ControlType controlType, IntPtr element, uint usage) : base(controlType)
    {
        Element = element;
        Usage = usage;
        Cookie = IOHIDElementGetCookie(element);
        Min = IOHIDElementGetLogicalMin(element);
        Max = IOHIDElementGetLogicalMax(element);
    }

    internal static HidElement CreateButton(IntPtr element, uint usage) =>
        new(ControlType.Button, element, usage);

    internal static HidElement CreateHat(IntPtr element, uint usage) =>
        new(ControlType.Hat, element, usage);

    internal static HidElement CreateAnalog(IntPtr element, uint usage) =>
        new(ControlType.Analog, element, usage);

    public override string ToString() => ControlType switch
    {
        ControlType.Button => $"Button {Usage}",
        ControlType.Analog => $"Stick 0x{Usage:x2}",
        ControlType.Hat => $"D-Pad",
        _ => string.Empty
    };
}
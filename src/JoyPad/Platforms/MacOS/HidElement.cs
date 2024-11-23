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

        Name = ControlType switch
        {
            ControlType.Button => $"Button {GetUsageName()}",
            ControlType.Analog => $"Stick {GetUsageName()}",
            ControlType.Hat => $"D-Pad",
            _ => string.Empty
        };
    }

    internal static HidElement CreateButton(IntPtr element, uint usage) =>
        new(ControlType.Button, element, usage);

    internal static HidElement CreateHat(IntPtr element, uint usage) =>
        new(ControlType.Hat, element, usage);

    internal static HidElement CreateAnalog(IntPtr element, uint usage) =>
        new(ControlType.Analog, element, usage);

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

    public override string ToString() => Name;
}
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using OldBit.Joypad.Controls;
using OldBit.Joypad.Platforms.MacOS.Extensions;
using OldBit.Joypad.Platforms.MacOS.Interop;
using static OldBit.Joypad.Platforms.MacOS.Interop.CoreFoundation;
using static OldBit.Joypad.Platforms.MacOS.Interop.IOKit;

namespace OldBit.Joypad.Platforms.MacOS;

[SupportedOSPlatform("macos")]
internal class HidController : JoypadController
{
    private readonly IntPtr _device;
    private readonly HashSet<IntPtr> _elements = [];

    internal HidController(IntPtr device)
    {
        _device = device;

        var name = IOHIDDeviceGetProperty(_device, kIOHIDProductKey).AsString();

        Name = name ?? "Unknown";
        Id = CreateDeviceUniqueId();
    }

    protected override int? GetValue(Control control)
    {
        var value = ((HidElement)control).GetValue();

        if (control.ControlType == ControlType.DirectionalPad)
        {
            return (int)GetDirectionalPadDirection(value);
        }

        return value;
    }

    private static DirectionalPadDirection GetDirectionalPadDirection(int? value) => value switch
    {
        0 => Joypad.Controls.DirectionalPadDirection.Up,
        1 => Joypad.Controls.DirectionalPadDirection.Up | Joypad.Controls.DirectionalPadDirection.Right,
        2 => Joypad.Controls.DirectionalPadDirection.Right,
        3 => Joypad.Controls.DirectionalPadDirection.Down | Joypad.Controls.DirectionalPadDirection.Right,
        4 => Joypad.Controls.DirectionalPadDirection.Down,
        5 => Joypad.Controls.DirectionalPadDirection.Down | Joypad.Controls.DirectionalPadDirection.Left,
        6 => Joypad.Controls.DirectionalPadDirection.Left,
        7 => Joypad.Controls.DirectionalPadDirection.Up | Joypad.Controls.DirectionalPadDirection.Left,
        _ => Joypad.Controls.DirectionalPadDirection.None
    };

    internal void ProcessElements()
    {
        using var elements = IOHIDDeviceCopyMatchingElements(_device, IntPtr.Zero).ToDisposable();

        ProcessElements(elements);
    }

    private Guid CreateDeviceUniqueId()
    {
        var vendorId = IOHIDDeviceGetProperty(_device, kIOHIDVendorIDKey).AsInt();
        var productId = IOHIDDeviceGetProperty(_device, kIOHIDProductIDKey).AsInt();
        var version = IOHIDDeviceGetProperty(_device, kIOHIDVersionNumberKey).AsInt();
        var transport = IOHIDDeviceGetProperty(_device, kIOHIDTransportKey).AsString();

        var guidBytes =
            BitConverter.GetBytes(vendorId ?? 0).Concat(
                BitConverter.GetBytes(productId ?? 0)).Concat(
                BitConverter.GetBytes(version ?? 0)).Concat(
                GenerateDeterministicHash($"{Name}:{transport}"));

        return new Guid(guidBytes.ToArray());
    }

    private void ProcessElements(IntPtr elements)
    {
        var hidElementTypeId = IOHIDElementGetTypeID();

        var count = CFArrayGetCount(elements);

        for (var i = 0; i < count; i++)
        {
            var element = CFArrayGetValueAtIndex(elements, i);
            var typeId = CFGetTypeID(element);

            if (typeId != hidElementTypeId)
            {
                continue;
            }

            var usagePage = IOHIDElementGetUsagePage(element);
            var elementType = IOHIDElementGetType(element);
            var usage = IOHIDElementGetUsage(element);

            switch (elementType)
            {
                case IOHIDElementType.Axis:
                case IOHIDElementType.Button:
                case IOHIDElementType.Misc:
                    HidElement control;

                    switch (usagePage)
                    {
                        case kHIDPage_GenericDesktop:
                            switch (usage)
                            {
                                case kHIDUsage_GD_X:
                                case kHIDUsage_GD_Y:
                                case kHIDUsage_GD_Z:
                                case kHIDUsage_GD_Rx:
                                case kHIDUsage_GD_Ry:
                                case kHIDUsage_GD_Rz:
                                    control = HidElement.CreateAnalog(_device, element, usage);
                                    AddControl(control);

                                    break;

                                case kHIDUsage_GD_Hatswitch:
                                    control = HidElement.CreateHat(_device, element, usage);
                                    AddControl(control);

                                    break;
                            }
                            break;

                        case kHIDPage_Button:
                            control = HidElement.CreateButton(_device, element, usage);
                            AddControl(control);

                            break;
                    }

                    break;

                case IOHIDElementType.Collection:
                    var children = IOHIDElementGetChildren(element);
                    ProcessElements(children);

                    break;
            }
        }
    }

    private void AddControl(HidElement control)
    {
        if (!_elements.Add(control.Element))
        {
            return;
        }

        AddControl((Control)control);
    }

    private static byte[] GenerateDeterministicHash(string s)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(s));

        return bytes[..4];
    }
}
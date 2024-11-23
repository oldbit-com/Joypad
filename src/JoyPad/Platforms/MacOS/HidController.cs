using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using OldBit.JoyPad.Platforms.MacOS.Extensions;
using OldBit.JoyPad.Platforms.MacOS.Interop;
using static OldBit.JoyPad.Platforms.MacOS.Interop.CoreFoundation;
using static OldBit.JoyPad.Platforms.MacOS.Interop.IOKit;

namespace OldBit.JoyPad.Platforms.MacOS;

[SupportedOSPlatform("macos")]
internal class HidController : Controller
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

    protected override int? GetControlValue(Control control) => GetValue((HidElement)control);

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

    private int? GetValue(HidElement control)
    {
        var value = IOHIDValueCreateWithIntegerValue(IntPtr.Zero, control.Element, 0, 0);

        unsafe
        {
            var result = IOHIDDeviceGetValue(_device, control.Element, &value);

            if (result == kIOReturnSuccess)
            {
                return IOHIDValueGetIntegerValue(value);
            }
        }

        return null;
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
                                    control = HidElement.CreateAnalog(element, usage);
                                    AddControl(control);

                                    break;

                                case kHIDUsage_GD_Hatswitch:
                                    control = HidElement.CreateHat(element, usage);
                                    AddControl(control);

                                    break;
                            }
                            break;

                        case kHIDPage_Button:
                            control = HidElement.CreateButton(element, usage);
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
using OldBit.Joypad.Controls;
using OldBit.Joypad.Platforms.Windows.Interop;
using System.Runtime.Versioning;
using static OldBit.Joypad.Platforms.Windows.Interop.XInput;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal class XInputController : JoypadController
{
    private readonly int _controllerIndex;
    private readonly XInputCapabilities _capabilities;
    private readonly HashSet<Control> _controls = [];

    private XInputGamepad? _state = null;

    internal XInputController(int controllerIndex, XInputCapabilities capabilities)
    {
        _controllerIndex = controllerIndex;
        _capabilities = capabilities;

        AddControls(capabilities);

        Name = CreateName();
        Id = CreateDeviceUniqueId();
    }

    protected override int? GetValue(Control control)
    {
        if (_state == null)
        {
            return null;
        }

        // Retrieve the value of the control from the current state

        return null;
    }

    protected override void UpdateState() =>
        _state = GetState(_controllerIndex);

    private void AddControls(XInputCapabilities capabilities)
    {
        if (HasButton(capabilities.Gamepad, XInputGamepadDPadUp))
        {
            var control = XInputControl.CreateButton(XInputGamepadDPadUp, "Directional Pad", DirectionalPadDirection.Up);
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadDPadDown))
        {
            var control = XInputControl.CreateButton(XInputGamepadDPadDown, "Directional Pad", DirectionalPadDirection.Down);
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadDPadLeft))
        {
            var control = XInputControl.CreateButton(XInputGamepadDPadLeft, "Directional Pad", DirectionalPadDirection.Left);
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadDPadRight))
        {
            var control = XInputControl.CreateButton(XInputGamepadDPadRight, "Directional Pad", DirectionalPadDirection.Right);
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadStart))
        {
            var control = XInputControl.CreateButton(XInputGamepadStart, "Start");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadBack))
        {
            var control = XInputControl.CreateButton(XInputGamepadBack, "Back");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadLeftThumb))
        {
            var control = XInputControl.CreateButton(XInputGamepadLeftThumb, "Left Thumb");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadRightThumb))
        {
            var control = XInputControl.CreateButton(XInputGamepadRightThumb, "Right Thumb");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadLeftShoulder))
        {
            var control = XInputControl.CreateButton(XInputGamepadLeftShoulder, "Left Shoulder");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadRightShoulder))
        {
            var control = XInputControl.CreateButton(XInputGamepadRightShoulder, "Right Shoulder");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadA))
        {
            var control = XInputControl.CreateButton(XInputGamepadA, "A");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadB))
        {
            var control = XInputControl.CreateButton(XInputGamepadB, "B");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadX))
        {
            var control = XInputControl.CreateButton(XInputGamepadX, "X");
            _controls.Add(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadY))
        {
            var control = XInputControl.CreateButton(XInputGamepadY, "Y");
            _controls.Add(control);
        }

        if (capabilities.Gamepad.ThumbLX != 0)
        {
            var control = XInputControl.CreateThumbStick(0x800001, "Left Thumb X");
            _controls.Add(control);
        }

        if (capabilities.Gamepad.ThumbLY != 0)
        {
            var control = XInputControl.CreateThumbStick(0x800002, "Left Thumb Y");
            _controls.Add(control);
        }

        if (capabilities.Gamepad.ThumbRX != 0)
        {
            var control = XInputControl.CreateThumbStick(0x800003, "Right Thumb X");
            _controls.Add(control);
        }

        if (capabilities.Gamepad.ThumbRY != 0)
        {
            var control = XInputControl.CreateThumbStick(0x800004, "Right Thumb Y");
            _controls.Add(control);
        }
    }

    private string CreateName()
    {
        var name = _capabilities.SubType switch
        {
            XInputDevSubTypeGamepad => "Gamepad",
            XInputDevSubTypeWheel => "Wheel",
            XInputDevSubTypeArcadeStick => "Arcade Stick",
            XInputDevSubTypeFlightStick => "Flight Stick",
            XInputDevSubTypeDancePad => "Dance Pad",
            XInputDevSubTypeGuitar => "Guitar",
            XInputDevSubTypeGuitarAlternate => "Guitar (Alternate)",
            XInputDevSubTypeDrumKit => "Drum Kit",
            XInputDevSubTypeGuitarBass => "Guitar Bass",
            XInputDevSubTypeArcadePad => "Arcade Pad",
            _ => "Unknown"
        };

        return $"{name} {_controllerIndex + 1}";
    }

    private Guid CreateDeviceUniqueId()
    {
        return new Guid(
            _capabilities.Gamepad.Buttons,
            (short)(_controllerIndex + 1),
            3,
            _capabilities.Type,
            _capabilities.SubType,
            6,
            7,
            8,
            9,
            _capabilities.Gamepad.LeftTrigger,
            _capabilities.Gamepad.RightTrigger);
    }

    private static XInputGamepad? GetState(int userIndex)
    {
        var result = XInputGetState(userIndex, out var state);

        return result == ErrorSuccess ? state.Gamepad : null;
    }

    private static bool HasButton(XInputGamepad gamepad, int button) =>
        (gamepad.Buttons & button) == button;
}

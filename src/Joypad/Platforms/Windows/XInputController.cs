using System.Runtime.Versioning;
using OldBit.Joypad.Controls;
using OldBit.Joypad.Platforms.Windows.Interop;
using static OldBit.Joypad.Platforms.Windows.Interop.XInput;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal class XInputController : JoypadController
{
    private const int LeftThumbX = 0x800001;
    private const int LeftThumbY = 0x800002;
    private const int RightThumbX = 0x800003;
    private const int RightThumbY = 0x800004;
    private const int LeftTrigger = 0x800005;
    private const int RightTrigger = 0x800006;

    private readonly int _controllerIndex;
    private readonly XInputCapabilities _capabilities;

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

        switch (control.Id)
        {
            case XInputGamepadDPadUp:
            case XInputGamepadDPadDown:
            case XInputGamepadDPadLeft:
            case XInputGamepadDPadRight:
            case XInputGamepadStart:
            case XInputGamepadBack:
            case XInputGamepadLeftThumb:
            case XInputGamepadRightThumb:
            case XInputGamepadLeftShoulder:
            case XInputGamepadRightShoulder:
            case XInputGamepadA:
            case XInputGamepadB:
            case XInputGamepadX:
            case XInputGamepadY:
                return GetButtonValue(XInputGamepadDPadUp);

            case LeftThumbX:
                return _state.Value.ThumbLX;

            case LeftThumbY:
                return _state.Value.ThumbLY;

            case RightThumbX:
                return _state.Value.ThumbRX;

            case RightThumbY:
                return _state.Value.ThumbRY;

            case LeftTrigger:
                return _state.Value.LeftTrigger;

            case RightTrigger:
                return _state.Value.RightTrigger;

            default:
                return null;

        }
    }

    protected override void UpdateState() => _state = GetState(_controllerIndex);

    private void AddControls(XInputCapabilities capabilities)
    {
        if (HasButton(capabilities.Gamepad, XInputGamepadDPadUp))
        {
            var control = XInputControl.CreateDirectionalPad(XInputGamepadDPadUp, "Directional Pad", DirectionalPadDirection.Up);
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadDPadDown))
        {
            var control = XInputControl.CreateDirectionalPad(XInputGamepadDPadDown, "Directional Pad", DirectionalPadDirection.Down);
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadDPadLeft))
        {
            var control = XInputControl.CreateDirectionalPad(XInputGamepadDPadLeft, "Directional Pad", DirectionalPadDirection.Left);
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadDPadRight))
        {
            var control = XInputControl.CreateDirectionalPad(XInputGamepadDPadRight, "Directional Pad", DirectionalPadDirection.Right);
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadStart))
        {
            var control = XInputControl.CreateButton(XInputGamepadStart, "Start");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadBack))
        {
            var control = XInputControl.CreateButton(XInputGamepadBack, "Back");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadLeftThumb))
        {
            var control = XInputControl.CreateButton(XInputGamepadLeftThumb, "Left Thumb");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadRightThumb))
        {
            var control = XInputControl.CreateButton(XInputGamepadRightThumb, "Right Thumb");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadLeftShoulder))
        {
            var control = XInputControl.CreateButton(XInputGamepadLeftShoulder, "Left Shoulder");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadRightShoulder))
        {
            var control = XInputControl.CreateButton(XInputGamepadRightShoulder, "Right Shoulder");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadA))
        {
            var control = XInputControl.CreateButton(XInputGamepadA, "A");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadB))
        {
            var control = XInputControl.CreateButton(XInputGamepadB, "B");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadX))
        {
            var control = XInputControl.CreateButton(XInputGamepadX, "X");
            AddControl(control);
        }

        if (HasButton(capabilities.Gamepad, XInputGamepadY))
        {
            var control = XInputControl.CreateButton(XInputGamepadY, "Y");
            AddControl(control);
        }

        if (capabilities.Gamepad.ThumbLX != 0)
        {
            var control = XInputControl.CreateThumbStick(LeftThumbX, "Left Thumb X");
            AddControl(control);
        }

        if (capabilities.Gamepad.ThumbLY != 0)
        {
            var control = XInputControl.CreateThumbStick(LeftThumbY, "Left Thumb Y");
            AddControl(control);
        }

        if (capabilities.Gamepad.ThumbRX != 0)
        {
            var control = XInputControl.CreateThumbStick(RightThumbX, "Right Thumb X");
            AddControl(control);
        }

        if (capabilities.Gamepad.ThumbRY != 0)
        {
            var control = XInputControl.CreateThumbStick(RightThumbY, "Right Thumb Y");
            AddControl(control);
        }

        if (capabilities.Gamepad.LeftTrigger != 0)
        {
            var control = XInputControl.CreateButton(LeftTrigger, "Left Trigger");
            AddControl(control);
        }

        if (capabilities.Gamepad.RightTrigger != 0)
        {
            var control = XInputControl.CreateButton(RightTrigger, "Right Trigger");
            AddControl(control);
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

    private int GetButtonValue(int controlId) =>
        ((_state?.Buttons ?? 0) & controlId) != 0 ? 1 : 0;
}

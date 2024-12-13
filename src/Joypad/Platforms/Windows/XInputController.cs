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
        var state = GetState(_controllerIndex);

        if (state == null)
        {
            return null;
        }

        return null;
    }

    private void AddControls(XInputCapabilities capabilities)
    {

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
}

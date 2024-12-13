using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace OldBit.Joypad.Platforms.Windows.Interop;

[SupportedOSPlatform("windows")]
internal static partial class XInput
{
    internal const int ErrorSuccess = 0x0000;
    internal const int ErrorDeviceNotConnected = 0x048F;

    internal const int XInputDevTypeGamepad = 0x0001;

    internal const int XInputDevSubTypeUnknown = 0x00;
    internal const int XInputDevSubTypeGamepad = 0x01;
    internal const int XInputDevSubTypeWheel = 0x02;
    internal const int XInputDevSubTypeArcadeStick = 0x03;
    internal const int XInputDevSubTypeFlightStick = 0x04;
    internal const int XInputDevSubTypeDancePad = 0x05;
    internal const int XInputDevSubTypeGuitar = 0x06;
    internal const int XInputDevSubTypeGuitarAlternate = 0x07;
    internal const int XInputDevSubTypeDrumKit = 0x08;
    internal const int XInputDevSubTypeGuitarBass = 0x0B;
    internal const int XInputDevSubTypeArcadePad = 0x13;

    internal const int XInputGamepadDPadUp = 0x0001;
    internal const int XInputGamepadDPadDown = 0x0002;
    internal const int XInputGamepadDPadLeft = 0x0004;
    internal const int XInputGamepadDPadRight = 0x0008;
    internal const int XInputGamepadStart = 0x0010;
    internal const int XInputGamepadBack = 0x0020;
    internal const int XInputGamepadLeftThumb = 0x0040;
    internal const int XInputGamepadRightThumb = 0x0080;
    internal const int XInputGamepadLeftShoulder = 0x0100;
    internal const int XInputGamepadRightShoulder = 0x0200;
    internal const int XInputGamepadA = 0x1000;
    internal const int XInputGamepadB = 0x2000;
    internal const int XInputGamepadX = 0x4000;
    internal const int XInputGamepadY = 0x8000;

    [LibraryImport("Xinput1_4.dll")]
    internal static partial int XInputGetState(int userIndex, out XInputState state);

    [LibraryImport("Xinput1_4.dll")]
    internal static partial int XInputGetCapabilities(int userIndex, int flags, out XInputCapabilities capabilities);
}

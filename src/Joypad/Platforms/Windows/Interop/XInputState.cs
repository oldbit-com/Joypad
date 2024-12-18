using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Windows.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct XInputState
{
    internal uint PacketNumber;

    internal XInputGamepad Gamepad;
}

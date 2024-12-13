using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Windows.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct XInputGamepad
{
    internal ushort Buttons;

    internal byte LeftTrigger;

    internal byte RightTrigger;

    internal short ThumbLX;

    internal short ThumbLY;

    internal short ThumbRX;

    internal short ThumbRY;
}

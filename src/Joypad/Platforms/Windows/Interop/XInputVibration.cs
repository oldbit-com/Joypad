using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Windows.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct XInputVibration
{
    internal ushort LeftMotorSpeed;
    
    internal ushort RightMotorSpeed;
}

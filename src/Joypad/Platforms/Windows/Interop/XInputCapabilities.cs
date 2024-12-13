using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Windows.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct XInputCapabilities
{
    internal byte Type;
    
    internal byte SubType;
    
    internal ushort Flags;
    
    internal XInputGamepad Gamepad;
    
    internal XInputVibration Vibration;
}

using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Linux.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct InputId
{
    internal UInt16 BusType;
    internal UInt16 Vendor;
    internal UInt16 Product;
    internal UInt16 Version;
}

[StructLayout(LayoutKind.Sequential)]
internal struct TimeVal
{
    internal long Seconds;
    internal long Microseconds;
}

[StructLayout(LayoutKind.Sequential)]
internal struct InputEvent
{
    internal TimeVal Time;
    internal UInt16 Type;
    internal UInt16 Code;
    internal Int32 Value;
}
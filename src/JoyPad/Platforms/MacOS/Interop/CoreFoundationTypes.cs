using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace OldBit.JoyPad.Platforms.MacOS.Interop;

[SupportedOSPlatform("macos")]
internal enum CFNumberType
{
    // Basic C int type.
    IntType = 9
}

[SupportedOSPlatform("macos")]
[StructLayout(LayoutKind.Sequential)]
internal struct CFRange(int location, int length)
{
    private IntPtr _location = location;
    private IntPtr _length = length;
}
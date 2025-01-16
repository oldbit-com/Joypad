using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace OldBit.Joypad.Platforms.Linux.Interop;

[SupportedOSPlatform("linux")]
internal static partial class LibC
{
    [LibraryImport("libc")]
    internal static unsafe partial int ioctl(IntPtr fd, CULong request, void* data);
}
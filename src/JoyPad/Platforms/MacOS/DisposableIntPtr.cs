using System.Runtime.Versioning;
using static OldBit.JoyPad.Platforms.MacOS.Interop.CoreFoundation;

namespace OldBit.JoyPad.Platforms.MacOS;

[SupportedOSPlatform("macos")]
internal readonly struct DisposableIntPtr : IDisposable
{
    internal readonly IntPtr Ptr = IntPtr.Zero;

    internal DisposableIntPtr(IntPtr ptr) => Ptr = ptr;

    public static implicit operator IntPtr(DisposableIntPtr ptr) => ptr.Ptr;

    public static implicit operator DisposableIntPtr(IntPtr ptr) => new(ptr);

    public void Dispose()
    {
        if (Ptr == IntPtr.Zero)
        {
            return;
        }

        CFRelease(Ptr);
    }
}
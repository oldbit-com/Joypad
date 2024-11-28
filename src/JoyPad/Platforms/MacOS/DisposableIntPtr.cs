using System.Runtime.Versioning;
using static OldBit.JoyPad.Platforms.MacOS.Interop.CoreFoundation;

namespace OldBit.JoyPad.Platforms.MacOS;

[SupportedOSPlatform("macos")]
internal readonly struct DisposableIntPtr : IDisposable
{
    private readonly IntPtr _ptr = IntPtr.Zero;

    internal DisposableIntPtr(IntPtr ptr) => _ptr = ptr;

    public static implicit operator IntPtr(DisposableIntPtr ptr) => ptr._ptr;

    public static implicit operator DisposableIntPtr(IntPtr ptr) => new(ptr);

    public void Dispose()
    {
        if (_ptr == IntPtr.Zero)
        {
            return;
        }

        CFRelease(_ptr);
    }
}
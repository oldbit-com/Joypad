using System.Runtime.Versioning;
using OldBit.JoyPad.Platforms.MacOS.Interop;
using static OldBit.JoyPad.Platforms.MacOS.Interop.CoreFoundation;

namespace OldBit.JoyPad.Platforms.MacOS.Extensions;

[SupportedOSPlatform("macos")]
internal static class IntPtrExtensions
{
    internal static int? AsInt(this IntPtr cfIntPtr)
    {
        var result = 0;

        if (CFNumberGetValue(cfIntPtr, CFNumberType.IntType, ref result))
        {
            return result;
        }

        return null;
    }

    internal static string? AsString(this IntPtr cStrPtr) => CFStringGetCharacters(cStrPtr);

    internal static DisposableIntPtr ToDisposable(this IntPtr ptr) => new(ptr);
}
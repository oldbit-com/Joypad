namespace OldBit.JoyPad.Platforms.MacOS.Interop;

internal static partial class CoreFoundation
{
    internal static IntPtr CFStringCreateWithCharacters(string s) =>
        CFStringCreateWithCharacters(IntPtr.Zero, s, s.Length);

    internal static IntPtr CFNumberCreate(ref int value) =>
        CFNumberCreate(IntPtr.Zero, CFNumberType.IntType, ref value);

    internal static IntPtr CFDictionaryCreateMutable() =>
        CFDictionaryCreateMutable(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);

    internal static void CFDictionaryAddValue(IntPtr dictionary, IntPtr key, int value)
    {
        var valuePtr = CFNumberCreate(ref value);

        CFDictionaryAddValue(dictionary, key, valuePtr);
    }

    internal static IntPtr CFArrayCreateMutable() =>
        CFArrayCreateMutable(IntPtr.Zero, 0, IntPtr.Zero);

    internal static string? CFStringGetCharacters(IntPtr s)
    {
        if (s == IntPtr.Zero)
        {
            return null;
        }

        var length = CFStringGetLength(s);
        var charsPtr = CFStringGetCharactersPtr(s);

        if (charsPtr != IntPtr.Zero)
        {
            unsafe
            {
                return new string((char*)charsPtr, 0, length);
            }
        }

        var range = new CFRange(0, length);
        var buffer = new char[length];

        // Need to call alternative method to get characters
        CFStringGetCharacters(s, range, buffer);

        return new string(buffer);
    }
}
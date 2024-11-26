namespace OldBit.JoyPad.Platforms.MacOS.Interop;

internal partial class IOKit
{
    private static IntPtr CreateOrGet(string s, ref IntPtr key)
    {
        if (key == IntPtr.Zero)
        {
            key = CoreFoundation.CFStringCreateWithCharacters(s);
        }

        return key;
    }
}
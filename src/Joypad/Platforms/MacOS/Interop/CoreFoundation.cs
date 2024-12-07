using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace OldBit.Joypad.Platforms.MacOS.Interop;

[SupportedOSPlatform("macos")]
internal static partial class CoreFoundation
{
    private const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial void CFRelease(IntPtr cf);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial void CFRetain(IntPtr cf);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial uint CFGetTypeID(IntPtr cf);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial IntPtr CFRunLoopGetCurrent();

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial void CFRunLoopRun();

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial void CFRunLoopStop(IntPtr runLoop);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial IntPtr CFNumberCreate(IntPtr allocator, CFNumberType numberType, ref int value);

    [LibraryImport(CoreFoundationLibrary)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool CFNumberGetValue(IntPtr number, CFNumberType numberType, ref int value);

    [LibraryImport(CoreFoundationLibrary, StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr CFStringCreateWithCharacters(IntPtr alloc, string chars, int numChars);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial int CFStringGetLength(IntPtr s);

    [LibraryImport(CoreFoundationLibrary, StringMarshalling = StringMarshalling.Utf16)]
    internal static partial void CFStringGetCharacters(IntPtr s, CFRange range, [Out] char[] buffer);

    [LibraryImport(CoreFoundationLibrary, StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr CFStringGetCharactersPtr(IntPtr s);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial IntPtr CFDictionaryCreateMutable(IntPtr allocator, int capacity, IntPtr keyCallbacks, IntPtr valueCallbacks);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial void CFDictionaryAddValue(IntPtr dictionary, IntPtr key, IntPtr value);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial IntPtr CFArrayCreateMutable(IntPtr allocator, int capacity, IntPtr callbacks);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial void CFArrayAppendValue(IntPtr array, IntPtr value);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial int CFArrayGetCount(IntPtr array);

    [LibraryImport(CoreFoundationLibrary)]
    internal static partial IntPtr CFArrayGetValueAtIndex(IntPtr array, int index);
}
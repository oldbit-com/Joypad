using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Windows.Interop;

internal static partial class DirectInput8
{
    internal static readonly Guid IID_IDirectInput8W = Guid.Parse("BF798031-483A-4DA2-AA99-5D64ED369700");

    internal const int DI_OK = 0;

    internal const int DI8DEVCLASS_ALL = 0;
    internal const int DI8DEVCLASS_DEVICE = 1;
    internal const int DI8DEVCLASS_POINTER = 2;
    internal const int DI8DEVCLASS_KEYBOARD = 3;
    internal const int DI8DEVCLASS_GAMECTRL = 4;

    internal const uint DIEDFL_ALLDEVICES = 0x00000000;
    internal const uint DIEDFL_ATTACHEDONLY = 0x00000001;
    internal const uint DIEDFL_FORCEFEEDBACK = 0x00000100;
    internal const uint DIEDFL_INCLUDEALIASES = 0x00010000;
    internal const uint DIEDFL_INCLUDEPHANTOMS = 0x00020000;
    internal const uint DIEDFL_INCLUDEHIDDEN = 0x00040000;

    [LibraryImport("dinput8")]
    internal unsafe static partial int DirectInput8Create(IntPtr hInst, UInt32 dwVersion, Guid* riidltf, IntPtr* ppvOut, IntPtr punkOuter);
}

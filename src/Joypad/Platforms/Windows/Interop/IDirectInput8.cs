using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace OldBit.Joypad.Platforms.Windows.Interop;

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate bool EnumDevicesCallback(IntPtr lpddi, IntPtr pvRef);

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
//[GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
[Guid(IID)]
internal partial interface IDirectInput8
{
    internal const string IID = "BF798031-483A-4DA2-AA99-5D64ED369700";

    [PreserveSig]
    int EnumDevices(int dwDevType, EnumDevicesCallback lpCallback, IntPtr pvRef, uint dwFlags);

    [PreserveSig]
    int RunControlPanel(IntPtr hwndOwner, uint dwFlags);

    unsafe void FindDevice(Guid* rguidClass, string ptszName, Guid* rguidInstance);
}

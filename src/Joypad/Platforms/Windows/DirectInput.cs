using OldBit.Joypad.Platforms.Windows.Interop;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using static OldBit.Joypad.Platforms.Windows.Interop.DirectInput8;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal sealed class DirectInput
{
    private readonly IDirectInput8 _directInput;
    
    private static readonly ComWrappers ComWrappers = new StrategyBasedComWrappers();

    internal DirectInput()
    {
        var handle = Marshal.GetHINSTANCE(typeof(DirectInput).Module);
        var riidltf = IID_IDirectInput8W;
        var directInputInstance = IntPtr.Zero;

        unsafe
        {
            var result = DirectInput8Create(handle, 0x0800, &riidltf, &directInputInstance, IntPtr.Zero);

            if (result != DI_OK)
            {
                throw new JoypadException("Failed to create DirectInput object.", result);
            }
        }

        // var _directInput = (IDirectInput8)Marshal.GetComInterfaceForObject(directInputInstance, typeof(IDirectInput8));
        _directInput = (IDirectInput8)Marshal.GetObjectForIUnknown(directInputInstance);
       // ComWrappers.RegisterForMarshalling(ComWrappers);
        //_directInput = (IDirectInput8)ComWrappers.GetOrCreateObjectForComInstance(directInputInstance, CreateObjectFlags.Unwrap);



        unsafe
        {
            var deviceId  = Guid.NewGuid();
            var devId = Guid.NewGuid();
            
            _directInput.FindDevice(&deviceId, "Name", &devId);
        }

        var hh = Process.GetCurrentProcess().Handle;
        var result1 = _directInput.RunControlPanel(IntPtr.Zero, 0);

        //_directInput = (IDirectInput8)ComWrappers.GetOrRegisterObjectForComInstance(directInputInstance, CreateObjectFlags.None);

    }

    internal void EnumDevices()
    {
        unsafe
        {
            var result = _directInput.EnumDevices(
                DI8DEVCLASS_GAMECTRL,
                Callback,
                IntPtr.Zero,
                DIEDFL_ATTACHEDONLY);
            //var result = IDirectInput8_EnumDevices6(_directInput, DI8DEVCLASS_GAMECTRL, &Callback, IntPtr.Zero, DIEDFL_ATTACHEDONLY);

            if (result != DI_OK)
            {
                throw new JoypadException("Failed to enumerate DirectInput devices.", result);
            }
        }
    }

    //[UnmanagedCallersOnly]
    private static bool Callback(IntPtr lpddi, IntPtr pvRef)
    {
        Console.WriteLine("Callback");

        return true;
    }
}

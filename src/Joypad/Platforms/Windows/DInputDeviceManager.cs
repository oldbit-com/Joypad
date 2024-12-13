using System.Runtime.Versioning;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal class DInputDeviceManager : IDeviceManager
{
    private readonly DirectInput _directInput;

    public event EventHandler<ControllerEventArgs>? ControllerAdded;
    public event EventHandler<ControllerEventArgs>? ControllerRemoved;

    public DInputDeviceManager()
    {
        _directInput = new DirectInput();
    }

    public void StartListener()
    {
        _directInput.EnumDevices();
    }

    public void StopListener()
    {
    }

    public void Dispose()
    {
    }
}

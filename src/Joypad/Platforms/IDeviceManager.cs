namespace OldBit.Joypad.Platforms;

internal interface IDeviceManager : IDisposable
{
    event EventHandler<ControllerEventArgs>? ControllerAdded;
    
    event EventHandler<ControllerEventArgs>? ControllerRemoved;

    void StartListener();

    void StopListener();
}
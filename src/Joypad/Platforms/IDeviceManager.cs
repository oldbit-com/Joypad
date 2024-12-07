namespace OldBit.Joypad.Platforms;

internal interface IDeviceManager : IDisposable
{
    void StartListener();

    void StopListener();
}
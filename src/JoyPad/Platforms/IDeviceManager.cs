namespace OldBit.JoyPad.Platforms;

internal interface IDeviceManager : IDisposable
{
    void StartListener();
}
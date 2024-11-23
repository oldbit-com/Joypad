using System.Runtime.Versioning;
using OldBit.JoyPad.Platforms;
using OldBit.JoyPad.Platforms.MacOS;

namespace OldBit.JoyPad;

public sealed class JoyPadManager : IDisposable
{
    private readonly IDeviceManager? _deviceManager;

    public List<Controller> Controllers { get; } = [];

    public event EventHandler<ControllerEventArgs>? ControllerConnected;
    public event EventHandler<ControllerEventArgs>? ControllerDisconnected;
    public event EventHandler<ErrorEventArgs>? ErrorOccurred;

    public JoyPadManager()
    {
        if (OperatingSystem.IsMacOS())
        {
            _deviceManager = CreateMacOSDeviceManager();
        }
        else if (OperatingSystem.IsWindows())
        {
            // TODO: Implement Windows device manager
        }
        else if (OperatingSystem.IsLinux())
        {
            // TODO: Implement Linux device manager
        }
        else
        {
            throw new PlatformNotSupportedException($"The {Environment.OSVersion.VersionString} platform is not supported.");
        }
    }

    public void StartListener() => _deviceManager?.StartListener();

    public void StopListener() => _deviceManager?.StopListener();

    [SupportedOSPlatform("macos")]
    private HidDeviceManager CreateMacOSDeviceManager()
    {
        var deviceManager = new HidDeviceManager();

        deviceManager.ControllerAdded += (_, e) =>
        {
            Controllers.Add(e.Controller);

            e.Controller.IsConnected = true;
            ControllerConnected?.Invoke(this, new ControllerEventArgs(e.Controller));
        };

        deviceManager.ControllerRemoved += (_, e) =>
        {
            var existingController = Controllers.FirstOrDefault(c => c.Id == e.Controller.Id);

            if (existingController == null)
            {
                return;
            }

            Controllers.Remove(existingController);

            e.Controller.IsConnected = false;
            ControllerDisconnected?.Invoke(this, new ControllerEventArgs(e.Controller));
        };

        deviceManager.ErrorOccurred += (sender, e) => ErrorOccurred?.Invoke(sender, e);

        return deviceManager;
    }

    public void Dispose()
    {
        _deviceManager?.Dispose();
    }
}
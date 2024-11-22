using System.Runtime.Versioning;
using OldBit.JoyPad.Platforms;
using OldBit.JoyPad.Platforms.MacOS;

namespace OldBit.JoyPad;

public class ControllerEventArgs(Controller controller) : EventArgs
{
    public Controller Controller { get; } = controller;
}

public sealed class ControllerManager
{
    private readonly IDeviceManager? _deviceManager;

    public List<Controller> Controllers { get; } = [];

    public event EventHandler<ControllerEventArgs>? ControllerConnected;
    public event EventHandler<ControllerEventArgs>? ControllerDisconnected;

    public ControllerManager()
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

    [SupportedOSPlatform("macos")]
    private HidDeviceManager CreateMacOSDeviceManager()
    {
        var deviceManager = new HidDeviceManager();

        deviceManager.ControllerAdded += (_, e) =>
        {
            Controllers.Add(e.Controller);
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
            ControllerDisconnected?.Invoke(this, new ControllerEventArgs(e.Controller));
        };

        return deviceManager;
    }
}
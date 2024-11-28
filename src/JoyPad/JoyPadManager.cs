using System.Runtime.Versioning;
using OldBit.JoyPad.Platforms;
using OldBit.JoyPad.Platforms.MacOS;

namespace OldBit.JoyPad;

public sealed class JoyPadManager : IDisposable
{
    private readonly IDeviceManager? _deviceManager;
    private readonly List<JoyPadController> _controllers = [];

    private bool _isStarted;

    public IReadOnlyList<JoyPadController> Controllers => _controllers;

    public event EventHandler<JoyPadControllerEventArgs>? ControllerConnected;
    public event EventHandler<JoyPadControllerEventArgs>? ControllerDisconnected;
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

    public void Start()
    {
        if (_isStarted)
        {
            return;
        }

        _deviceManager?.StartListener();
        _isStarted = true;
    }

    public void Stop()
    {
        if (!_isStarted)
        {
            return;
        }

        _deviceManager?.StopListener();
        _isStarted = false;
    }

    public void Update(Guid controllerId)
    {
        var controller = Controllers.FirstOrDefault(c => c.Id == controllerId);

        if (controller is not { IsConnected: true })
        {
            return;
        }

        foreach (var control in controller.Controls)
        {
            controller.Update(control);
        }
    }

    [SupportedOSPlatform("macos")]
    private HidDeviceManager CreateMacOSDeviceManager()
    {
        var deviceManager = new HidDeviceManager();

        deviceManager.ControllerAdded += (_, e) =>
        {
            _controllers.Add(e.Controller);

            e.Controller.IsConnected = true;
            ControllerConnected?.Invoke(this, new JoyPadControllerEventArgs(e.Controller));
        };

        deviceManager.ControllerRemoved += (_, e) =>
        {
            var existingController = Controllers.FirstOrDefault(c => c.Id == e.Controller.Id);

            if (existingController == null)
            {
                return;
            }

            _controllers.Remove(existingController);

            e.Controller.IsConnected = false;
            ControllerDisconnected?.Invoke(this, new JoyPadControllerEventArgs(e.Controller));
        };

        deviceManager.ErrorOccurred += (sender, e) => ErrorOccurred?.Invoke(sender, e);

        return deviceManager;
    }

    public void Dispose()
    {
        _deviceManager?.Dispose();
    }
}
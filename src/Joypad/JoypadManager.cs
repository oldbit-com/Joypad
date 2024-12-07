using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;
using OldBit.Joypad.Platforms;
using OldBit.Joypad.Platforms.MacOS;

namespace OldBit.Joypad;

public sealed class JoypadManager : IDisposable
{
    private readonly IDeviceManager? _deviceManager;
    private readonly List<JoypadController> _controllers = [];

    private bool _isStarted;

    public IReadOnlyList<JoypadController> Controllers => _controllers;

    public event EventHandler<JoypadControllerEventArgs>? ControllerConnected;
    public event EventHandler<JoypadControllerEventArgs>? ControllerDisconnected;
    public event EventHandler<ErrorEventArgs>? ErrorOccurred;

    public JoypadManager()
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

    public bool TryGetController(Guid controllerId, [NotNullWhen(true)] out JoypadController? controller)
    {
        controller = Controllers.FirstOrDefault(c => c.Id == controllerId);

        return controller != null;
    }

    [SupportedOSPlatform("macos")]
    private HidDeviceManager CreateMacOSDeviceManager()
    {
        var deviceManager = new HidDeviceManager();

        deviceManager.ControllerAdded += (_, e) =>
        {
            _controllers.Add(e.Controller);

            e.Controller.IsConnected = true;
            ControllerConnected?.Invoke(this, new JoypadControllerEventArgs(e.Controller));
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
            ControllerDisconnected?.Invoke(this, new JoypadControllerEventArgs(e.Controller));
        };

        deviceManager.ErrorOccurred += (sender, e) => ErrorOccurred?.Invoke(sender, e);

        return deviceManager;
    }

    public void Dispose()
    {
        _deviceManager?.Dispose();
    }
}
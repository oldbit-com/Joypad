using System.Diagnostics.CodeAnalysis;
using OldBit.Joypad.Platforms;
using OldBit.Joypad.Platforms.MacOS;
using OldBit.Joypad.Platforms.Windows;

namespace OldBit.Joypad;

public sealed class JoypadManager : IDisposable
{
    private readonly IDeviceManager? _deviceManager;
    private readonly List<JoypadController> _controllers = [];

    private bool _isStarted;

    public IReadOnlyList<JoypadController> Controllers => _controllers;

    public event EventHandler<JoypadControllerEventArgs>? ControllerConnected;
    public event EventHandler<JoypadControllerEventArgs>? ControllerDisconnected;

    public JoypadManager()
    {
        if (OperatingSystem.IsMacOS())
        {
            _deviceManager = new HidDeviceManager();
        }
        else if (OperatingSystem.IsWindows())
        {
            _deviceManager = new XInputDeviceManager();
        }
        else if (OperatingSystem.IsLinux())
        {
            // TODO: Implement Linux device manager
            throw new NotImplementedException();
        }
        else
        {
            throw new PlatformNotSupportedException($"The {Environment.OSVersion.VersionString} platform is not supported.");
        }

        _deviceManager.ControllerAdded += (_, e) =>
        {
            _controllers.Add(e.Controller);

            e.Controller.IsConnected = true;
            ControllerConnected?.Invoke(this, new JoypadControllerEventArgs(e.Controller));
        };

        _deviceManager.ControllerRemoved += (_, e) =>
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

        controller.Update();
    }

    public bool TryGetController(Guid controllerId, [NotNullWhen(true)] out JoypadController? controller)
    {
        controller = Controllers.FirstOrDefault(c => c.Id == controllerId);

        return controller != null;
    }

    public void Dispose()
    {
        _deviceManager?.Dispose();
    }
}
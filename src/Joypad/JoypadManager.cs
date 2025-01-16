using System.Diagnostics.CodeAnalysis;
using OldBit.Joypad.Platforms;
using OldBit.Joypad.Platforms.Linux;
using OldBit.Joypad.Platforms.MacOS;
using OldBit.Joypad.Platforms.Windows;

namespace OldBit.Joypad;

/// <summary>
/// Represents a manager for game controllers.
/// </summary>
public sealed class JoypadManager : IDisposable
{
    private readonly IDeviceManager? _deviceManager;
    private readonly List<JoypadController> _controllers = [];

    private bool _isStarted;

    /// <summary>
    /// Gets the controllers connected to the system.
    /// </summary>
    public IReadOnlyList<JoypadController> Controllers => _controllers;

    /// <summary>
    /// Event raised when a controller is connected.
    /// </summary>
    public event EventHandler<JoypadControllerEventArgs>? ControllerConnected;

    /// <summary>
    /// Event raised when a controller is disconnected.
    /// </summary>
    public event EventHandler<JoypadControllerEventArgs>? ControllerDisconnected;

    /// <summary>
    /// Creates a new instance of the <see cref="JoypadManager"/> class.
    /// </summary>
    /// <exception cref="PlatformNotSupportedException">Thrown when platform is not supported.
    /// Currently only macOS, Windows and Linux systems are supported.</exception>
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
            _deviceManager = new EventDeviceManager();
        }
        else
        {
            throw new PlatformNotSupportedException($"The {Environment.OSVersion.VersionString} platform is not supported.");
        }

        _deviceManager.ControllerAdded += (_, e) =>
        {
            _controllers.Add(e.Controller);
            e.Controller.IsConnected = true;
            e.Controller.Initialize();

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
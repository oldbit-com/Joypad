using OldBit.Joypad.Platforms.Windows.Interop;
using System.Runtime.Versioning;
using System.Timers;
using Timer = System.Timers.Timer;
using static OldBit.Joypad.Platforms.Windows.Interop.XInput;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal class XInputDeviceManager : IDeviceManager
{
    private const int MaxControllers = 4;

    private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(4);
    private readonly Timer _devicePollingTimer;
    private readonly XInputController?[] _controllers = new XInputController[MaxControllers];

    public event EventHandler<ControllerEventArgs>? ControllerAdded;
    public event EventHandler<ControllerEventArgs>? ControllerRemoved;

    public XInputDeviceManager()
    {
        _devicePollingTimer = new Timer(_pollingInterval)
        {
            AutoReset = true
        };

        _devicePollingTimer.Elapsed += CheckControllers;
    }

    public void StartListener() => _devicePollingTimer.Start();

    public void StopListener() => _devicePollingTimer.Stop();

    private void CheckControllers(object? sender, ElapsedEventArgs e)
    {
        for (var userIndex = 0; userIndex < MaxControllers; userIndex++)
        {
            var capabilities = GetCapabilities(userIndex);

            if (capabilities == null && _controllers[userIndex] != null)
            {
                ControllerRemoved?.Invoke(this, new ControllerEventArgs(_controllers[userIndex]!));
                _controllers[userIndex] = null;
            }

            if (capabilities != null && _controllers[userIndex] == null)
            {
                var controller = new XInputController(userIndex, capabilities.Value);
                _controllers[userIndex] = controller;

                ControllerAdded?.Invoke(this, new ControllerEventArgs(controller));
            }
        }
    }

    private static XInputCapabilities? GetCapabilities(int userIndex)
    {
        var result = XInputGetCapabilities(userIndex, XInputDevTypeGamepad, out var capabilities);

        return result == ErrorSuccess ? capabilities : null;
    }

    public void Dispose() => _devicePollingTimer.Dispose();
}

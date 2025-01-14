using System.Runtime.Versioning;
using System.Timers;
using Timer = System.Timers.Timer;

namespace OldBit.Joypad.Platforms.Linux;

[SupportedOSPlatform("linux")]
internal class EventDeviceManager : IDeviceManager
{
    private readonly Dictionary<string, EventDevice?> _eventDevices = [];

    private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(5);
    private readonly Timer _devicePollingTimer;

    public event EventHandler<ControllerEventArgs>? ControllerAdded;
    public event EventHandler<ControllerEventArgs>? ControllerRemoved;

    public EventDeviceManager()
    {
        _devicePollingTimer = new Timer(_pollingInterval)
        {
            AutoReset = false
        };

        _devicePollingTimer.Elapsed += CheckControllers;
    }

    public void StartListener() => _devicePollingTimer.Start();

    public void StopListener() => _devicePollingTimer.Stop();

    private void CheckControllers(object? sender, ElapsedEventArgs e)
    {
        RefreshInputDevices();
    }

    private void RefreshInputDevices()
    {
        var devicesPaths = GetInputDevices();
        var knownDevices = new HashSet<string>(_eventDevices.Keys);

        foreach (var devicePath in devicesPaths)
        {
            knownDevices.Remove(devicePath);

            if (_eventDevices.ContainsKey(devicePath))
            {
                continue;
            }

            try
            {
                var eventDevice = new EventDevice(devicePath);
                eventDevice.Disconnected += EventDeviceOnDisconnected;

                if (eventDevice is { IsOpened: true, IsGamepad: true })
                {
                    _eventDevices[devicePath] = eventDevice;
                    ControllerAdded?.Invoke(this, new ControllerEventArgs(eventDevice));
                }
                else
                {
                    _eventDevices[devicePath] = null;
                }
            }
            catch
            {
                _eventDevices[devicePath] = null;
            }
        }

        foreach (var path in knownDevices)
        {
            var eventDevice = _eventDevices[path];

            if (eventDevice != null)
            {
                RemoveDevice(eventDevice);
            }
        }
    }

    private void RemoveDevice(EventDevice eventDevice)
    {
        ControllerRemoved?.Invoke(this, new ControllerEventArgs(eventDevice));
        eventDevice.Dispose();

        _eventDevices.Remove(eventDevice.DevicePath);
    }

    private void EventDeviceOnDisconnected(object? sender, ControllerEventArgs e)
    {
        if (e.Controller is not EventDevice eventDevice)
        {
            return;
        }

        RemoveDevice(eventDevice);
    }

    private static IEnumerable<string> GetInputDevices()
    {
        try
        {
            return Directory.EnumerateFiles("/dev/input", "event*");
        }
        catch
        {
            return [];
        }
    }

    public void Dispose()
    {
        _devicePollingTimer.Dispose();

        foreach (var device in _eventDevices.Values)
        {
            device?.Dispose();
        }
    }
}
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using OldBit.Joypad.Controls;
using OldBit.Joypad.Platforms.Linux.Interop;
using static OldBit.Joypad.Platforms.Linux.Interop.IOCtl;
using static OldBit.Joypad.Platforms.Linux.Interop.LibC;

namespace OldBit.Joypad.Platforms.Linux;

[SupportedOSPlatform("linux")]
internal class EventDevice : JoypadController, IDisposable
{
    internal event EventHandler<ControllerEventArgs>? Disconnected;

    private readonly FileStream? _deviceStream;
    private readonly IntPtr _fd;

    private readonly Dictionary<EventCode, Dictionary<int, int?>> _controlValues = new()
    {
        [EventCode.EV_KEY] = new Dictionary<int, int?>(),
        [EventCode.EV_ABS] = new Dictionary<int, int?>()
    };
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private bool _disposed;

    internal string DevicePath { get; }

    internal bool IsOpened => _deviceStream != null;
    internal bool IsGamepad => Controls.Count > 0;

    internal EventDevice(string path)
    {
        DevicePath = path;

        _deviceStream = OpenDevice();

        if (_deviceStream == null)
        {
            return;
        }

        _fd = _deviceStream.SafeFileHandle.DangerousGetHandle();

        Name = GetDeviceName();
        Id = CreateDeviceUniqueId();

        ProcessCapabilities();

        Task.Factory.StartNew(async() =>
        {
            await DeviceReadWorker(_cancellationTokenSource.Token);
        }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    protected override int? GetValue(Control control)
    {
        var eventControl = (EventDeviceControl)control;
        var controls = _controlValues[eventControl.EventCode];

        return controls.GetValueOrDefault(eventControl.Code);
    }

    private FileStream? OpenDevice()
    {
        try
        {
            return File.Open(DevicePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch
        {
            return null;
        }
    }

    private async Task DeviceReadWorker(CancellationToken cancellationToken)
    {
        var buffer = new byte[Marshal.SizeOf(typeof(InputEvent))];

        var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var bufferPtr = bufferHandle.AddrOfPinnedObject();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                _ = await _deviceStream!.ReadAsync(buffer, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch
            {
                Disconnected?.Invoke(this, new ControllerEventArgs(this));
                break;
            }

            var inputEvent = (InputEvent)Marshal.PtrToStructure(bufferPtr, typeof(InputEvent))!;

            if (inputEvent.Type is (int)EventCode.EV_KEY or (int)EventCode.EV_ABS)
            {
                _controlValues[(EventCode)inputEvent.Type][inputEvent.Code] = inputEvent.Value;
            }
        }

        bufferHandle.Free();
    }

    private string GetDeviceName()
    {
        string? name;

        unsafe
        {
            const int len = 256;

            var buffer = stackalloc byte[len];
            var result = ioctl(_fd, new CULong(EVIOCGNAME(len)), buffer);

            if (result < 0)
            {
                throw new JoypadException("Failed to get device name", result);
            }

            name = Marshal.PtrToStringAnsi(new IntPtr(buffer));
        }

        return string.IsNullOrEmpty(name) ? $"Device {Path.GetFileName(DevicePath)[5..]}" : name;
    }

    private void ProcessCapabilities()
    {
        var capabilities = ProcessCapabilities(EventCode.EV_KEY, KeyCode.KEY_MAX);
        ProcessKeyControls(capabilities, KeyCode.KEY_MAX);

        capabilities = ProcessCapabilities(EventCode.EV_ABS, AbsCode.ABS_MAX);
        ProcessAbsControls(capabilities, AbsCode.ABS_MAX);
    }

    private byte[] ProcessCapabilities(EventCode eventCode, int maxCount)
    {
        var capabilities = new byte[maxCount / 8 + 1];

        unsafe
        {
            fixed (byte* arr = capabilities)
            {
                var result = ioctl(_fd, new CULong(EVIOCGBIT(eventCode, maxCount)), arr);

                if (result < 0)
                {
                    throw new JoypadException("Failed to get device capabilities", result);
                }
            }
        }

        return capabilities;
    }

    private void ProcessKeyControls(byte[] capabilities, int keyCount)
    {
        for (var code = 0; code < keyCount; code++)
        {
            var isAvailable = ((capabilities[code / 8] >> code % 8) & 1) != 0;

            if (!isAvailable)
            {
                continue;
            }

            switch (code)
            {
                case KeyCode.BTN_TRIGGER:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Trigger"));
                    break;

                case KeyCode.BTN_A:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "A"));
                    break;

                case KeyCode.BTN_B:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "B"));
                    break;

                case KeyCode.BTN_C:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "C"));
                    break;

                case KeyCode.BTN_X:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "X"));
                    break;

                case KeyCode.BTN_Y:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Y"));
                    break;

                case KeyCode.BTN_Z:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Z"));
                    break;

                case KeyCode.BTN_TL:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Left Trigger"));
                    break;

                case KeyCode.BTN_TR:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Right Trigger"));
                    break;

                case KeyCode.BTN_TL2:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Left Trigger 2"));
                    break;

                case KeyCode.BTN_TR2:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Right Trigger 2"));
                    break;

                case KeyCode.BTN_SELECT:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Select"));
                    break;

                case KeyCode.BTN_START:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Start"));
                    break;

                case KeyCode.BTN_MODE:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Mode"));
                    break;

                case KeyCode.BTN_THUMBL:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Left Thumb Button"));
                    break;

                case KeyCode.BTN_THUMBR:
                    AddControl(new EventDeviceControl(ControlType.Button, EventCode.EV_KEY, code, "Right Thumb Button"));
                    break;
            }
        }
    }

    private void ProcessAbsControls(byte[] capabilities, int absCount)
    {
        for (var code = 0; code < absCount; code++)
        {
            var isAvailable = ((capabilities[code / 8] >> code % 8) & 1) != 0;

            if (!isAvailable)
            {
                continue;
            }

            switch (code)
            {
                case AbsCode.ABS_X:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, EventCode.EV_ABS, code, "Left Thumb X"));
                    break;

                case AbsCode.ABS_Y:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, EventCode.EV_ABS, code, "Left Thumb Y"));
                    break;

                case AbsCode.ABS_Z:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, EventCode.EV_ABS, code, "Left Thumb Z"));
                    break;

                case AbsCode.ABS_RX:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, EventCode.EV_ABS, code, "Right Thumb X"));
                    break;

                case AbsCode.ABS_RY:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, EventCode.EV_ABS, code, "Right Thumb Y"));
                    break;

                case AbsCode.ABS_RZ:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, EventCode.EV_ABS, code, "Right Thumb Z"));
                    break;

                case AbsCode.ABS_HAT0X:
                    // Directional pad X axis
                    break;

                case AbsCode.ABS_HAT0Y:
                    // Directional pad y axis
                    break;
            }
        }
    }

    private Guid CreateDeviceUniqueId()
    {
        InputId inputId;

        unsafe
        {
            var result = ioctl(_fd, new CULong(EVIOCGID), &inputId);

            if (result < 0)
            {
                throw new JoypadException("Failed to get device id", result);
            }
        }

        var guidBytes =
            BitConverter.GetBytes((int)inputId.Vendor).Concat(
                BitConverter.GetBytes((int)inputId.Product)).Concat(
                BitConverter.GetBytes((int)inputId.Version)).Concat(
                GenerateDeterministicHash($"{Name}:{inputId.BusType}"));

        return new Guid(guidBytes.ToArray());
    }

    private static byte[] GenerateDeterministicHash(string s)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(s));

        return bytes[..4];
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _cancellationTokenSource.Cancel();

        _deviceStream?.Dispose();
        _disposed = true;
    }
}
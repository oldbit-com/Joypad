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
    internal event EventHandler<ControllerEventArgs>? DeviceDisconnected;

    private readonly string _path;
    private readonly FileStream? _deviceStream;
    private readonly IntPtr _fd;

    private readonly byte[] _buffer = new byte[Marshal.SizeOf(typeof(InputEvent))];
    private readonly GCHandle? _bufferHandle;
    private readonly IntPtr _bufferPtr;

    private bool _disposed;

    internal bool IsOpened => _deviceStream != null;
    internal bool IsGamepad => Controls.Count > 0;

    internal EventDevice(string path)
    {
        _path = path;

        _deviceStream = OpenDevice();

        if (_deviceStream == null)
        {
            return;
        }

        _fd = _deviceStream.SafeFileHandle.DangerousGetHandle();
        _bufferHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
        _bufferPtr = _bufferHandle.Value.AddrOfPinnedObject();

        Name = GetDeviceName();
        Id = CreateDeviceUniqueId();

        ProcessCapabilities();

        Read();
    }

    protected override int? GetValue(Control control)
    {
        return null;
    }

    private FileStream? OpenDevice()
    {
        try
        {
            return File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch
        {
            return null;
        }
    }

    private void Read()
    {
        while (true)
        {
            var count = _deviceStream?.Read(_buffer);

            var inputEvent = (InputEvent)Marshal.PtrToStructure(_bufferPtr, typeof(InputEvent))!;
            if (inputEvent.Code > 0 || inputEvent.Type > 0 || inputEvent.Value > 0)
            {
                Console.WriteLine($"Event: Code: {inputEvent.Code} Type: {inputEvent.Type} Value: {inputEvent.Value}");
            }
        }
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

        return string.IsNullOrEmpty(name) ? $"Device {Path.GetFileName(_path)[5..]}" : name;
    }

    private void ProcessCapabilities()
    {
        var capabilities = ProcessCapabilities(EventCode.EV_KEY, (int)KeyCode.KEY_MAX);
        ProcessKeyControls(capabilities, (int)KeyCode.KEY_MAX);

        capabilities = ProcessCapabilities(EventCode.EV_ABS, (int)AbsCode.ABS_MAX);
        ProcessAbsControls(capabilities, (int)AbsCode.ABS_MAX);
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

            if (code is < (int)KeyCode.BTN_JOYSTICK or > (int)KeyCode.BTN_THUMBR)
            {
                continue;
            }

            switch ((KeyCode)code)
            {
                case KeyCode.BTN_TRIGGER:
                    AddControl(new EventDeviceControl(ControlType.Button, "Trigger"));
                    break;

                case KeyCode.BTN_A:
                    AddControl(new EventDeviceControl(ControlType.Button, "A"));
                    break;

                case KeyCode.BTN_B:
                    AddControl(new EventDeviceControl(ControlType.Button, "B"));
                    break;

                case KeyCode.BTN_C:
                    AddControl(new EventDeviceControl(ControlType.Button, "C"));
                    break;

                case KeyCode.BTN_X:
                    AddControl(new EventDeviceControl(ControlType.Button, "X"));
                    break;

                case KeyCode.BTN_Y:
                    AddControl(new EventDeviceControl(ControlType.Button, "Y"));
                    break;

                case KeyCode.BTN_Z:
                    AddControl(new EventDeviceControl(ControlType.Button, "Z"));
                    break;

                case KeyCode.BTN_TL:
                    AddControl(new EventDeviceControl(ControlType.Button, "Left Trigger"));
                    break;

                case KeyCode.BTN_TR:
                    AddControl(new EventDeviceControl(ControlType.Button, "Right Trigger"));
                    break;

                case KeyCode.BTN_TL2:
                    AddControl(new EventDeviceControl(ControlType.Button, "Left Trigger 2"));
                    break;

                case KeyCode.BTN_TR2:
                    AddControl(new EventDeviceControl(ControlType.Button, "Right Trigger 2"));
                    break;

                case KeyCode.BTN_SELECT:
                    AddControl(new EventDeviceControl(ControlType.Button, "Select"));
                    break;

                case KeyCode.BTN_START:
                    AddControl(new EventDeviceControl(ControlType.Button, "Start"));
                    break;

                case KeyCode.BTN_MODE:
                    AddControl(new EventDeviceControl(ControlType.Button, "Mode"));
                    break;

                case KeyCode.BTN_THUMBL:
                    AddControl(new EventDeviceControl(ControlType.Button, "Left Thumb Button"));
                    break;

                case KeyCode.BTN_THUMBR:
                    AddControl(new EventDeviceControl(ControlType.Button, "Right Thumb Button"));
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

            Console.WriteLine($"Abs Code: 0x{code:X2}");
            if (code > (int)AbsCode.ABS_HAT0Y)
            {
                continue;
            }

            switch ((AbsCode)code)
            {
                case AbsCode.ABS_X:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, "Left Thumb X"));
                    break;

                case AbsCode.ABS_Y:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, "Left Thumb Y"));
                    break;

                case AbsCode.ABS_Z:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, "Left Thumb Z"));
                    break;

                case AbsCode.ABS_RX:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, "Right Thumb X"));
                    break;

                case AbsCode.ABS_RY:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, "Right Thumb Y"));
                    break;

                case AbsCode.ABS_RZ:
                    AddControl(new EventDeviceControl(ControlType.ThumbStick, "Right Thumb Z"));
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

        _bufferHandle?.Free();
        _deviceStream?.Dispose();
        _disposed = true;
    }
}
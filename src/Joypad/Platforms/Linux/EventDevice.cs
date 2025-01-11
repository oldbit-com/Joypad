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
    private readonly string _path;
    private readonly FileStream? _deviceStream;
    private readonly IntPtr _fd;

    internal EventDevice(string path)
    {
        _path = path;

        _deviceStream = OpenDevice();

        if (_deviceStream == null)
        {
            return;
        }

        _fd = _deviceStream.SafeFileHandle.DangerousGetHandle();

        Name = GetDeviceName();
        Id = CreateDeviceUniqueId();

        ProcessCapabilities();
    }

    public bool IsOpened => _deviceStream != null;

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
        const int bitCount = (int)KeyCode.KEY_MAX;
        var bits = new byte[bitCount / 8 + 1];

        unsafe
        {
            fixed (byte* arr = bits)
            {
                var result = ioctl(_fd, new CULong(EVIOCGBIT(EventCode.EV_KEY, bitCount)), arr);

                if (result < 0)
                {
                    throw new JoypadException("Failed to get device capabilities", result);
                }
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

    public void Dispose() => _deviceStream?.Dispose();
}
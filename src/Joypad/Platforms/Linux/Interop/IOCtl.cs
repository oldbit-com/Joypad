// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
using System.Runtime.InteropServices;

namespace OldBit.Joypad.Platforms.Linux.Interop;

internal static class IOCtl
{
    private const int _IOC_SIZEBITS = 14;
    private const int _IOC_DIRBITS = 2;
    private const int _IOC_NRBITS = 8;
    private const int _IOC_TYPEBITS = 8;

    private const int _IOC_NRMASK = (1 << _IOC_NRBITS) - 1;
    private const int _IOC_TYPEMASK = (1 << _IOC_TYPEBITS) - 1;
    private const int _IOC_SIZEMASK = (1 << _IOC_SIZEBITS) - 1;
    private const int _IOC_DIRMASK = (1 << _IOC_DIRBITS) - 1;

    private const int _IOC_NONE = 0;
    private const int _IOC_WRITE = 1;
    private const int _IOC_READ = 2;

    private const int _IOC_NRSHIFT = 0;
    private const int _IOC_TYPESHIFT = _IOC_NRSHIFT + _IOC_NRBITS;
    private const int _IOC_SIZESHIFT = _IOC_TYPESHIFT + _IOC_TYPEBITS;
    private const int _IOC_DIRSHIFT = _IOC_SIZESHIFT + _IOC_SIZEBITS;

    private static uint _IOC(long dir, long type, long nr, long size) =>
        (uint)(dir << _IOC_DIRSHIFT |
               type << _IOC_TYPESHIFT |
               nr << _IOC_NRSHIFT |
               size << _IOC_SIZESHIFT);

    private static uint _IO(long type, long nr) => _IOC(_IOC_NONE, type, nr, 0);
    private static uint _IOR(long type, long nr, long size) => _IOC(_IOC_READ, type, nr, size);
    private static uint _IOW(long type, long nr, long size) => _IOC(_IOC_WRITE, type, nr, size);

    internal static readonly uint EVIOCGID = _IOR('E', 0x02, Marshal.SizeOf(typeof(InputId)));
    internal static uint EVIOCGNAME(int len) => _IOC(_IOC_READ, 'E', 0x06, len);
    internal static uint EVIOCGBIT(EventCode ev, int len) => _IOC(_IOC_READ, 'E', (long)(0x20 + ev), len);
}

[StructLayout(LayoutKind.Sequential)]
internal struct InputId
{
    internal UInt16 BusType;
    internal UInt16 Vendor;
    internal UInt16 Product;
    internal UInt16 Version;
}
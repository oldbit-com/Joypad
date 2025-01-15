using OldBit.Joypad.Controls;
using OldBit.Joypad.Platforms.Linux.Interop;

namespace OldBit.Joypad.Platforms.Linux;

public class EventDeviceControl : Control
{
    internal EventCode EventCode { get; }
    internal int Code { get; }

    internal EventDeviceControl(
        ControlType controlType,
        EventCode eventCode,
        int code,
        string name) : base(controlType)
    {
        Name = name;
        EventCode = eventCode;
        Code = code;
    }
}
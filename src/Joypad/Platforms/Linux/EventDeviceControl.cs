using OldBit.Joypad.Controls;
using OldBit.Joypad.Platforms.Linux.Interop;

namespace OldBit.Joypad.Platforms.Linux;

public class EventDeviceControl : Control
{
    internal EventDeviceControl(
        ControlType controlType,
        EventCode eventCode,
        string name) : base(controlType)
    {
        Name = name;
    }
}
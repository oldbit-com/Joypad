using OldBit.Joypad.Controls;

namespace OldBit.Joypad.Platforms.Linux;

public class EventDeviceControl : Control
{
    internal EventDeviceControl(ControlType controlType, string name) : base(controlType)
    {
        Name = name;
    }
}
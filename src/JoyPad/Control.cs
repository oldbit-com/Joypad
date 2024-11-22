namespace OldBit.JoyPad;

public abstract class Control(ControlType controlType)
{
    public ControlType ControlType { get; } = controlType;
}
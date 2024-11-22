namespace OldBit.JoyPad;

public class Control
{
    public ControlType ControlType { get; }

    public Control(ControlType controlType)
    {
        ControlType = controlType;
    }
}
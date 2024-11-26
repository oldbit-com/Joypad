namespace OldBit.JoyPad;

public abstract class Control(ControlType controlType)
{
    public ControlType ControlType { get; } = controlType;

    public int Id { get; protected set; }

    public string Name { get; protected set; } = string.Empty;
}
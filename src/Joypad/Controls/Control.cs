namespace OldBit.Joypad.Controls;

public abstract class Control(ControlType controlType)
{
    public ControlType ControlType { get; } = controlType;

    public int Id { get; protected set; }

    public string Name { get; protected init; } = string.Empty;

    public int? Value { get; internal set; }

    public bool IsPressed => Value > 0;
}
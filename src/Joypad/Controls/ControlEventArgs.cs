namespace OldBit.Joypad.Controls;

public class ControlEventArgs(Control control) : EventArgs
{
    public Control Control { get; } = control;
}
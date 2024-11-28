namespace OldBit.JoyPad;

public class ControlEventArgs(Control control) : EventArgs
{
    public Control Control { get; } = control;
}
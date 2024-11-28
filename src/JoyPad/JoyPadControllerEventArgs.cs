namespace OldBit.JoyPad;

public class JoyPadControllerEventArgs(JoyPadController controller) : EventArgs
{
    public JoyPadController Controller { get; } = controller;
}
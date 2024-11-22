namespace OldBit.JoyPad;

public class ControllerEventArgs(Controller controller) : EventArgs
{
    public Controller Controller { get; } = controller;
}
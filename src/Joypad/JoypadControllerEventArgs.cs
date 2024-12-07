namespace OldBit.Joypad;

public class JoypadControllerEventArgs(JoypadController controller) : EventArgs
{
    public JoypadController Controller { get; } = controller;
}
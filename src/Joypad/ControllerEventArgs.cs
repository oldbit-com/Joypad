namespace OldBit.Joypad;

internal class ControllerEventArgs(JoypadController controller) : EventArgs
{
    public JoypadController Controller { get; } = controller;
}

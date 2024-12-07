namespace OldBit.Joypad;

public class ErrorEventArgs(Exception exception) : EventArgs
{
    public Exception Exception { get; } = exception;
}
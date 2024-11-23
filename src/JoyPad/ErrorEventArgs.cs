namespace OldBit.JoyPad;

public class ErrorEventArgs(Exception exception) : EventArgs
{
    public Exception Exception { get; } = exception;
}
namespace OldBit.JoyPad;

public class JoyPadException : Exception
{
    public int StatusCode { get; }

    public JoyPadException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public JoyPadException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
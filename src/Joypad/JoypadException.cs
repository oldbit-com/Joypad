namespace OldBit.Joypad;

public class JoypadException : Exception
{
    public int StatusCode { get; }

    public JoypadException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public JoypadException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
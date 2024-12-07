namespace OldBit.Joypad.Platforms.MacOS.Interop;

public enum IOHIDElementType : uint
{
    Misc = 1,
    Button = 2,
    Axis = 3,
    ScanCodes = 4,
    Output = 129,
    Feature = 257,
    Collection = 513,
    Null = 5,
}
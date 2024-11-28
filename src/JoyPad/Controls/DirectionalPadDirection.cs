namespace OldBit.JoyPad.Controls;

/// <summary>
/// Lists the possible directions of a DPad.
/// </summary>
[Flags]
public enum DirectionalPadDirection
{
    /// <summary>
    /// No button is pressed.
    /// </summary>
    None = 0x00,

    /// <summary>
    /// The up button is pressed.
    /// </summary>
    Up = 0x01,

    /// <summary>
    /// The right button is pressed.
    /// </summary>
    Right = 0x02,

    /// <summary>
    /// The down button is pressed.
    /// </summary>
    Down = 0x04,

    /// <summary>
    /// The left button is pressed.
    /// </summary>
    Left = 0x08,
}
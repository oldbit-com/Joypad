namespace OldBit.JoyPad;

/// <summary>
/// Lists the possible directions of a DPad.
/// </summary>
public enum DPadDirection
{
    /// <summary>
    /// The top button is pressed.
    /// </summary>
    Top = 0,

    /// <summary>
    /// The top right button is pressed.
    /// </summary>
    TopRight = 1,

    /// <summary>
    /// The right button is pressed.
    /// </summary>
    Right = 2,

    /// <summary>
    /// The bottom right button is pressed.
    /// </summary>
    BottomRight = 3,

    /// <summary>
    /// The bottom button is pressed.
    /// </summary>
    Bottom = 4,

    /// <summary>
    /// The bottom left button is pressed.
    /// </summary>
    BottomLeft = 5,

    /// <summary>
    /// The left button is pressed.
    /// </summary>
    Left = 6,

    /// <summary>
    /// The top left button is pressed.
    /// </summary>
    TopLeft = 7,

    /// <summary>
    /// No button is pressed.
    /// </summary>
    None = 15,
}
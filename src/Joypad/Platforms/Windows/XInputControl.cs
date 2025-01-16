using System.Runtime.Versioning;
using OldBit.Joypad.Controls;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal class XInputControl : Control
{
    private XInputControl(ControlType controlType, int inputId, string name, DirectionalPadDirection direction = DirectionalPadDirection.None) : base(controlType)
    {
        Name = name;
        Id = inputId;
    }

    internal static XInputControl CreateButton(int inputId, string name) =>
        new(ControlType.Button, inputId, name);

    internal static XInputControl CreateThumbStick(int inputId, string name) =>
        new(ControlType.ThumbStick, inputId, name);

    internal static XInputControl CreateDirectionalPad(int inputId, string name, DirectionalPadDirection direction) =>
        new(ControlType.ThumbStick, inputId, name, direction);
}
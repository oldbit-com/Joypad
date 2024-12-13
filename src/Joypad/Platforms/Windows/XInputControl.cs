using System.Runtime.Versioning;
using OldBit.Joypad.Controls;

namespace OldBit.Joypad.Platforms.Windows;

[SupportedOSPlatform("windows")]
internal class XInputControl : Control
{
    private DirectionalPadDirection _direction;

    private XInputControl(ControlType controlType, int inputId, string name, DirectionalPadDirection direction = DirectionalPadDirection.None) : base(controlType)
    {
        Name = name;
        _direction = direction;

        Id = inputId;
    }

    internal static XInputControl CreateButton(int inputId, string name, DirectionalPadDirection direction = DirectionalPadDirection.None) =>
        new(ControlType.Button, inputId, name, direction);


    internal static XInputControl CreateThumbStick(int inputId, string name) =>
        new(ControlType.ThumbStick, inputId, name);
}
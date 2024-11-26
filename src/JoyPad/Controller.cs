namespace OldBit.JoyPad;

public abstract class Controller
{
    private readonly List<Control> _controls = [];
    private Control? _dpad = null;

    public IReadOnlyList<Control> Controls => _controls;

    public Guid Id { get; protected init; } = Guid.Empty;
    public string Name { get; internal set; } = string.Empty;
    public bool IsConnected { get; internal set; }

    internal Controller()
    {
    }

    public DPadDirection GetDPadValue()
    {
        if (_dpad == null)
        {
            return DPadDirection.None;
        }

        var value = GetValue(_dpad);

        return value switch
        {
            >= 0 and <= 7 or 15 => (DPadDirection)value,
            _ => DPadDirection.None
        };
    }

    public int? GetValue(Control control) => !IsConnected ? null : GetControlValue(control);

    protected abstract int? GetControlValue(Control control);

    public bool HasDPad => _dpad != null;

    internal void AddControl(Control control)
    {
        _controls.Add(control);

        if (control.ControlType == ControlType.Hat)
        {
            _dpad = control;
        }
    }
}
namespace OldBit.JoyPad;

public abstract class JoyPadController
{
    private readonly List<Control> _controls = [];
    private Control? _dpad;

    /// <summary>
    /// Occurs when a control value changes.
    /// </summary>
    public event EventHandler<ControlEventArgs>? ValueChanged;

    /// <summary>
    /// Gets the controls of the controller.
    /// </summary>
    public IReadOnlyList<Control> Controls => _controls;

    /// <summary>
    /// Gets the unique identifier of the controller.
    /// </summary>
    public Guid Id { get; protected init; } = Guid.Empty;

    /// <summary>
    /// Gets the name of the controller.
    /// </summary>
    public string Name { get; internal init; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the controller is connected.
    /// </summary>
    public bool IsConnected { get; internal set; }

    internal JoyPadController()
    {
    }

    public DirectionalPadDirection DirectionalPadDirection
    {
        get
        {
            if (_dpad == null)
            {
                return DirectionalPadDirection.None;
            }

            var value = GetValue(_dpad);

            return value == null ?
                DirectionalPadDirection.None :
                GetDirectionalPadDirection(value.Value);
        }
    }

    internal void Update(Control control)
    {
        if (!IsConnected)
        {
            return;
        }

        var value = GetValue(control);

        if (value == control.Value)
        {
            return;
        }

        control.Value = value;

        ValueChanged?.Invoke(this, new ControlEventArgs(control));
    }

    internal void Initialize()
    {
        foreach (var control in _controls)
        {
            control.Value = GetValue(control);
        }
    }

    protected abstract int? GetValue(Control control);

    protected abstract DirectionalPadDirection GetDirectionalPadDirection(int value);

    public bool HasDPad => _dpad != null;

    internal void AddControl(Control control)
    {
        _controls.Add(control);

        if (control.ControlType == ControlType.DirectionalPad)
        {
            _dpad = control;
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using OldBit.Joypad.Controls;

namespace OldBit.Joypad;

public abstract class JoypadController
{
    private readonly List<Control> _controls = [];
    private readonly Dictionary<int, Control> _controlsById = [];

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

    internal JoypadController()
    {
    }

    internal void Update()
    {
        UpdateState();

        foreach (var control in Controls)
        {
            ProcessControl(control);
        }
    }

    private void ProcessControl(Control control)
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

    public bool TryGetControl(int controlId, [NotNullWhen(true)] out Control? control) =>
        _controlsById.TryGetValue(controlId, out  control);

    internal void Initialize()
    {
        UpdateState();

        foreach (var control in _controls)
        {
            control.Value = GetValue(control);
        }
    }

    protected virtual void UpdateState() { }

    protected abstract int? GetValue(Control control);

    internal void AddControl(Control control)
    {
        _controls.Add(control);
        _controlsById[control.Id] = control;
    }
}
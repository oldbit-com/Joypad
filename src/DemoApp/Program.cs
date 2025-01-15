using OldBit.Joypad;
using OldBit.Joypad.Controls;

// Create a new instance of the JoypadManager class.
var manager = new JoypadManager();

// Current controller.
JoypadController? controller = null;

// Handle controller connected event
manager.ControllerConnected += (_, e) =>
{
    Console.WriteLine($"Connected: {e.Controller.Name} / {e.Controller.Id}");

    controller = e.Controller;
    controller.ValueChanged -= ControllerOnValueChanged;
    controller.ValueChanged += ControllerOnValueChanged;

    return;

    // Handle controller value changed event. Events are triggered by manager.Update(controller.Id)
    void ControllerOnValueChanged(object? sender, ControlEventArgs args)
    {
        Console.WriteLine($"Value changed: {args.Control.Name} = {args.Control.Value}");
    }
};

// Handle controller disconnected event
manager.ControllerDisconnected += (_, e) =>
{
    Console.WriteLine($"Disconnected: {e.Controller.Name} / {e.Controller.Id}");

    controller = null;
};

var cancellationToken = new CancellationTokenSource();

// Run main loop that will update controller state periodically
_ = Task.Factory.StartNew(async() =>
{
    while (!cancellationToken.IsCancellationRequested)
    {
        await Task.Delay(100);

        if (controller is not { IsConnected: true })
        {
            continue;
        }

        // Update controller state.
        // This will also trigger any events for any controls that value has changed.
        // Alternatively you can access values as: controller.Controls[n].Value
        manager.Update(controller.Id);
    }
}, cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

// Start the manager - this will start listening for controller events
manager.Start();

Console.WriteLine("Press ENTER to exit");
Console.ReadLine();

// Stop the manager and cancel the main loop
manager.Stop();
cancellationToken.Cancel();

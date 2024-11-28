using OldBit.JoyPad;
using OldBit.JoyPad.Controls;

var manager = new JoyPadManager();

JoyPadController? controller = null;
manager.ControllerConnected += (_, e) =>
{
    Console.WriteLine($"Connected: {e.Controller.Name} / {e.Controller.Id}");

    controller = e.Controller;
    controller.ValueChanged += ControllerOnValueChanged;

    return;

    void ControllerOnValueChanged(object? sender, ControlEventArgs args)
    {
        Console.WriteLine($"Value changed: {args.Control.Name} = {args.Control.Value}");
    }
};

manager.ControllerDisconnected += (_, e) =>
{
    Console.WriteLine($"Disconnected: {e.Controller.Name} / {e.Controller.Id}");

    controller = null;
};

manager.ErrorOccurred += (_, e) =>
{
    Console.WriteLine(e.Exception.ToString());
};

var cancellationToken = new CancellationTokenSource();
_ = Task.Factory.StartNew(() =>
{
    while (true)
    {
        Thread.Sleep(100);

        if (controller is not { IsConnected: true })
        {
            continue;
        }

        // Update controller state.
        // This will also trigger events for any controls that value has changed.
        // Alternatively you can can access values as: controller.Controls[n].Value
        manager.Update(controller.Id);
    }
}, cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

manager.Start();

Console.WriteLine("Press ENTER to exit");
Console.ReadLine();

manager.Stop();
cancellationToken.Cancel();

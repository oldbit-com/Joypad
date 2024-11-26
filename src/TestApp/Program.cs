using OldBit.JoyPad;

var manager = new JoyPadManager();


Controller? controller = null;
manager.ControllerConnected += (_, e) =>
{
    controller = e.Controller;
};

manager.ControllerDisconnected += (_, _) =>
{
    controller = null;
};

manager.ErrorOccurred += (_, e) =>
{
    Console.WriteLine(e.Exception.ToString());
};

var mainLoop = new Thread(() =>
{
    var currentValues = new Dictionary<Control, int?>();
    var dpadValue = DPadDirection.None;

    while (true)
        //while (e.Controller.IsConnected)
    {
        Thread.Sleep(50);

        if (controller is not { IsConnected: true })
        {
            continue;
        }

        var dpad = controller.GetDPadValue();

        if (dpad != dpadValue)
        {
            dpadValue = dpad;

            Console.WriteLine($"DPad: {dpadValue}");

            continue;
        }


        // foreach (var control in currentController.Controls)
        // {
        //     var value = currentController.GetValue(control);
        //
        //     if (currentValues.TryGetValue(control, out var currentValue) && currentValue == value)
        //     {
        //         continue;
        //     }
        //
        //     currentValues[control] = value;
        //
        //     Console.WriteLine($"{control}: {value}");
        // }
    }

}) { IsBackground = true };

manager.Start();
mainLoop.Start();


Console.WriteLine("Started");

Console.ReadLine();

manager.Stop();
using OldBit.JoyPad;

var manager = new JoyPadManager();


Controller? currentController = null;
manager.ControllerConnected += (_, e) =>
{
    currentController = e.Controller;
};

manager.ControllerDisconnected += (_, _) =>
{
    currentController = null;
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

        if (currentController is not { IsConnected: true })
        {
            continue;
        }

        var dpad = currentController.GetDPadValue();

        if (dpad != dpadValue)
        {
            dpadValue = dpad;

            Console.WriteLine($"DPad: {dpadValue}");

            continue;
        }


        foreach (var control in currentController.Controls)
        {
            var value = currentController.GetValue(control);

            if (currentValues.TryGetValue(control, out var currentValue) && currentValue == value)
            {
                continue;
            }

            currentValues[control] = value;

            Console.WriteLine($"{control}: {value}");
        }
    }

}) { IsBackground = true };

manager.StartListener();
mainLoop.Start();


Console.WriteLine("Started");

Console.ReadLine();

manager.StopListener();
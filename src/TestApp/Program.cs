using OldBit.JoyPad;

var manager = new ControllerManager();

var currentValues = new Dictionary<Control, int?>();

manager.ControllerConnected += (sender, e) =>
{
    var dpadValue = DPadDirection.None;

    while (true)
    {
        Thread.Sleep(50);

        foreach (var control in e.Controller.Controls)
        {
            var xxx = e.Controller.GetDPadValue();
            if (xxx != dpadValue)
            {
                dpadValue = xxx;
                Console.WriteLine($"DPad: {dpadValue}");
            }


            continue;

            var value = e.Controller.GetValue(control);

            if (currentValues.TryGetValue(control, out var currentValue) && currentValue == value)
            {
                continue;
            }

            currentValues[control] = value;

            Console.WriteLine($"{control}: {value}");
        }
    }
};

manager.StartListener();

Console.ReadLine();
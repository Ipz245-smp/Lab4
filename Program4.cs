using System;

class Program4
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Завдання 1: Ланцюжок відповідальностей");
        var support = new SupportSystem();
        support.Run();

        Console.WriteLine("\nЗавдання 2: Посередник");
        var commandCentre = new CommandCentre();

        var runway1 = new Runway("R1", commandCentre);
        var runway2 = new Runway("R2", commandCentre);
        commandCentre.AddRunway(runway1);
        commandCentre.AddRunway(runway2);

        var boeing  = new Aircraft("Boeing-737", commandCentre);
        var airbus  = new Aircraft("Airbus-A320", commandCentre);
        var cessna  = new Aircraft("Cessna-172", commandCentre);

        boeing.Land();
        airbus.Land();
        cessna.Land();
        boeing.Takeoff();

        Console.WriteLine("\nЗавдання 3: Спостерігач (EventListener)");

        var button = new LightElementNode("button", DisplayType.Inline, ClosingType.WithClosing, "btn");
        button.AddChild(new LightTextNode("Click me"));

        button.AddEventListener("click", e => Console.WriteLine($"  → Обробник 1: кнопку натиснули! (подія: {e})"));
        button.AddEventListener("click", e => Console.WriteLine($"  → Обробник 2: логування кліку"));
        button.AddEventListener("mouseover", e => Console.WriteLine($"  → Обробник: курсор над кнопкою"));

        Console.WriteLine($"HTML: {button.OuterHTML}");
        button.DispatchEvent("click");
        button.DispatchEvent("mouseover");
        button.DispatchEvent("keydown");

        Console.WriteLine("\nЗавдання 4: Стратегія (Image)");

        var div = new LightElementNodeS("div", "gallery");

        var localImg   = new LightImageNode("images/photo.jpg", "Local photo");
        var networkImg = new LightImageNode("https://example.com/pic.png", "Network photo");

        div.AddChild(localImg);
        div.AddChild(networkImg);

        Console.WriteLine(div.OuterHTML);

        Console.WriteLine("\nЗавдання 5: Мементо");

        var editor = new TextEditor("Привіт");
        editor.Type(", світ");
        editor.Type("! Як справи?");
        editor.Type(" Все добре.");
        editor.Undo();
        editor.Undo();
        editor.ShowCurrent();
        editor.Undo();
        editor.ShowCurrent();
        editor.Undo();
    }
}

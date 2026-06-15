using System;
using System.Collections.Generic;

interface ICommandCentre
{
    void RequestLanding(Aircraft aircraft);
    void RequestTakeoff(Aircraft aircraft);
    void RunwayFree(Runway runway);
}

class Aircraft
{
    public string Name { get; }
    private ICommandCentre _commandCentre;

    public Aircraft(string name, ICommandCentre commandCentre)
    {
        Name = name;
        _commandCentre = commandCentre;
    }

    public void Land()
    {
        Console.WriteLine($"{Name}: Запит на посадку...");
        _commandCentre.RequestLanding(this);
    }

    public void Takeoff()
    {
        Console.WriteLine($"{Name}: Запит на зліт...");
        _commandCentre.RequestTakeoff(this);
    }

    public void NotifyLanded()   => Console.WriteLine($"{Name}: Посадка виконана.");
    public void NotifyTakenOff() => Console.WriteLine($"{Name}: Зліт виконано.");
    public void NotifyWaiting()  => Console.WriteLine($"{Name}: Очікую дозволу...");
}

class Runway
{
    public string Name { get; }
    public bool IsAvailable { get; private set; } = true;
    private ICommandCentre _commandCentre;

    public Runway(string name, ICommandCentre commandCentre)
    {
        Name = name;
        _commandCentre = commandCentre;
    }

    public void Occupy()
    {
        IsAvailable = false;
        Console.WriteLine($"Злітна смуга {Name}: зайнята.");
    }

    public void Free()
    {
        IsAvailable = true;
        Console.WriteLine($"Злітна смуга {Name}: вільна.");
        _commandCentre.RunwayFree(this);
    }
}

class CommandCentre : ICommandCentre
{
    private List<Runway> _runways = new List<Runway>();
    private Queue<(Aircraft aircraft, string action)> _waitingQueue = new Queue<(Aircraft, string)>();

    public void AddRunway(Runway runway) => _runways.Add(runway);

    public void RequestLanding(Aircraft aircraft)
    {
        var runway = _runways.Find(r => r.IsAvailable);
        if (runway != null)
        {
            runway.Occupy();
            aircraft.NotifyLanded();
            runway.Free();
        }
        else
        {
            aircraft.NotifyWaiting();
            _waitingQueue.Enqueue((aircraft, "land"));
        }
    }

    public void RequestTakeoff(Aircraft aircraft)
    {
        var runway = _runways.Find(r => r.IsAvailable);
        if (runway != null)
        {
            runway.Occupy();
            aircraft.NotifyTakenOff();
            runway.Free();
        }
        else
        {
            aircraft.NotifyWaiting();
            _waitingQueue.Enqueue((aircraft, "takeoff"));
        }
    }

    public void RunwayFree(Runway runway)
    {
        if (_waitingQueue.Count > 0)
        {
            var (aircraft, action) = _waitingQueue.Dequeue();
            Console.WriteLine($"CommandCentre: Дозвіл надано для {aircraft.Name}");
            if (action == "land")  RequestLanding(aircraft);
            else                   RequestTakeoff(aircraft);
        }
    }
}

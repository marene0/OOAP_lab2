using System;
using System.Collections.Generic;

interface IArmy
{
    string Name { get; }
    void DisplayHierarchy(string prefix = "");
    void StepForward();
}

class Soldier : IArmy
{
    public string Name { get; }
    private bool _isSteppingForward = false;

    public Soldier(string name)
    {
        Name = name;
    }

    public void DisplayHierarchy(string prefix = "")
    {
        string status = _isSteppingForward ? " (Stepping Forward)" : "";
        Console.WriteLine($"{prefix}- {Name} (Soldier){status}");
    }

    public void StepForward()
    {
        _isSteppingForward = true;
    }
}

class Platoon : IArmy
{
    private List<IArmy> _components = new List<IArmy>();
    public string Name { get; }

    public Platoon(string name)
    {
        Name = name;
    }

    public void Add(IArmy component)
    {
        _components.Add(component);
    }

    public void Remove(IArmy component)
    {
        _components.Remove(component);
    }

    public void DisplayHierarchy(string prefix = "")
    {
        Console.WriteLine($"{prefix}+ {Name} (Platoon)");
        prefix += "    ";
        foreach (var component in _components)
        {
            component.DisplayHierarchy(prefix);
        }
    }

    public void StepForward()
    {
        foreach (var component in _components)
        {
            if (component is Soldier)
            {
                ((Soldier)component).StepForward();
            }
            else if (component is Platoon)
            {
                ((Platoon)component).StepForward();
            }
        }
    }

    public void StepForwardAll()
    {
        foreach (var component in _components)
        {
            if (component is Soldier)
            {
                ((Soldier)component).StepForward();
            }
            else if (component is Platoon)
            {
                ((Platoon)component).StepForwardAll();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var Soldier1 = new Soldier("Ivan");
        var Soldier2 = new Soldier("Oleg");
        var Soldier3 = new Soldier("Roman");
        var Soldier4 = new Soldier("Andii");


        var Platoon1 = new Platoon("92 OMBp");
        Platoon1.Add(Soldier1);
        Platoon1.Add(Soldier2);

        var Platoon2 = new Platoon("127 OBp");
        Platoon2.Add(Soldier3);
        Platoon2.Add(Soldier4);


        var root = new Platoon("Army");
        root.Add(Platoon1);
        root.Add(Platoon2);

        root.DisplayHierarchy();

        Console.WriteLine("\nSelecting one soldier to step forward:");
        Soldier1.StepForward();
        root.DisplayHierarchy();

        Console.WriteLine("\nSelecting all platoons to step forward:");
        root.StepForwardAll();
        root.DisplayHierarchy();
    }
}

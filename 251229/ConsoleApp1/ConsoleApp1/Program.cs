using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        Queue<Unit> barracks = new();
        
        barracks.Enqueue(new Unit("Marine"));
        barracks.Enqueue(new Unit("Ghost"));
        barracks.Enqueue(new Unit("Firebat"));
        barracks.Enqueue(new Unit("Medic"));
        barracks.Enqueue(new Unit("Marine"));

        Console.WriteLine(barracks.Count);
        
        Unit unit = barracks.Dequeue();

        Console.WriteLine("--------------------");

        foreach (Unit u in barracks)
        {
            Console.WriteLine(u.Name);
        }
    }
}

public class Unit
{
    public string Name { get; private set; }

    public Unit(string name)
    {
        Name = name;
    }
}
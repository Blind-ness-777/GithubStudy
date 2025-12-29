using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        
        
    }

    static void PrintList(LinkedList<int> list)
    {
        foreach (int l in list)
        {
            Console.Write($"[{l}]");
        }

        Console.WriteLine();
    }
}

public class Monster
{
    public string Name { get; private set; }

    public Monster(string name)
    {
        Name = name;
    }
}
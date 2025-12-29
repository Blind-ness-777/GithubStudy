using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        
        List<Monster> list = new List<Monster>();

        list.Add(new Monster("오크"));
        PrintList(list);

        Monster slime1 = new("슬라임");
        list.Add(slime1);
        PrintList(list);
        
        list.Add(new Monster("해골"));
        PrintList(list);
        
        list.Insert(1, new Monster("아이루"));
        PrintList(list);

        Console.WriteLine(list[2].Name);

        Monster slime2 = new("슬라임");
        if (list.Contains(slime1))
        {
            Console.WriteLine("슬라임 찾았음");
        }

        list.Remove(slime1);
        PrintList(list);
        
        list.RemoveAt(0);
        PrintList(list);
    }

    static void PrintList(List<Monster> list)
    {
        foreach (Monster l in list)
        {
            Console.Write($"[{l.Name}]");
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
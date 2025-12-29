using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
    
        LinkedList<int> list = new LinkedList<int>();
    
        LinkedListNode<int> node = list.AddFirst(1);   // [1]
        list.AddFirst(2);   // [2][1]
        list.AddFirst(3);   // [3][2][1]
        list.AddLast(4);    // [3][2][1][4]
    
        list.AddBefore(node, 5);
        // [3][2][5][1][4]
    
        list.AddAfter(node, 6);
        // [3][2][5][1][6][4]
    
        list.Remove(5);
        // 실제 데이터가 아닌 것을 타겟으로 할 경우 아무 일도 일어나지 않는다.
        // [3][2][1][6][4]
        list.Remove(node);
        // [3][2][6][4]
    
        list.Remove(list.First);
        // [2][6][4]
        list.RemoveLast();
        // [2][6]

        Console.WriteLine($"list Count : {list.Count}");

        if (list.Contains(2))
        {
            Console.WriteLine("2 찾음");
        }
    
        PrintList(list);
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
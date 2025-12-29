using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
    
        Stack<int> stack = new();
        
// 데이터 추가
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);

// 맨 위에 있는 데이터 확인"만"
        Console.WriteLine(stack.Peek());

// 맨 위에 있는 데이터를 반환하고 스택에서 삭제
        Console.WriteLine(stack.Pop());
        Console.WriteLine();

        foreach (int i in stack)
        {
            Console.WriteLine(i);
        }

        stack.Contains(3);

        Console.WriteLine(stack.Count);
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
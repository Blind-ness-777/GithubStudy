using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        VendingMachine machine = new VendingMachine();

        int value = 0;

        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("숫자를 입력해주세요.");
                continue;
            }
            
            if (value <= 0)
            {
                Console.WriteLine("1 이상의 숫자를 입력해주세요.");
                continue;
            }

            break;
        }
        
        for (int i = 0; i < value; i++)
        {
            machine.AddMilk(new Milk());
        }
    }
}

public class Milk
{
    public int ExpirationDate { get; private set; }
    
    public Milk()
    {
        ExpirationDate = 7;
    }
}

public class VendingMachine
{
    Queue<Milk> milkQueue = new Queue<Milk>();

    public void AddMilk(Milk milk)
    {
        milkQueue.Enqueue(milk);
        Console.WriteLine("우유가 보충되었습니다.");
        Console.WriteLine($"남은 우유 : {milkQueue.Count}");
    }

    public void RemoveMilk()
    {
        if (milkQueue.Count <= 0)
        {
            Console.WriteLine("자판기에 우유가 없습니다.");
            return;
        }
        
        Milk milk = milkQueue.Dequeue();
        
        Console.WriteLine($"꺼낸 우유의 유통기한 : {milk.ExpirationDate}");
        
        Console.WriteLine($"자판기에 우유가 {milkQueue.Count}개 남아 있습니다.");
    }
}
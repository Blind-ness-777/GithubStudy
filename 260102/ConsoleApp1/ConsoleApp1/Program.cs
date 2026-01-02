namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        List<int> list = new(100000);

        for (int i = 1; i <= 100000; i++)
        {
            list.Add(i);
            Console.WriteLine($"add : {i}");
        }

        int findValue = new Random().Next(0, list.Count);

        BinarySearch(list, findValue);
    }

    static void BinarySearch(List<int> list, int target)
    {
        int count = 0;
        int start = 0;
        int end = list.Count - 1;

        while (start <= end)
        {
            count++;
            int pivot = start + (end - start) / 2;

            if (list[pivot] == target)
            {
                Console.WriteLine($"찾았음. count : {count}");
                return;
            }

            if (list[pivot] < target) start = pivot + 1;
            else end = pivot - 1;
        }
    }
}
    
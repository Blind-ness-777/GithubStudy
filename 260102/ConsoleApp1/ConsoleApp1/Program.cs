namespace ConsoleApp1;

class Program
{
    // 일반 함수
    static void Main(string[] args)
    {
        List<int> list = new();
        list.Add(64);
        list.Add(59);
        list.Add(71);
        list.Add(15);
        list.Add(23);
        
        BubbleSrot(list);
        Console.WriteLine();
        foreach (int i in list)
        {
            Console.Write($"[{i}]");
        }
    }
    
    // 버블 정렬
    static void BubbleSrot(List<int> list)
    {
        bool isSorted = false;
        
        for (int i = 0; i < list.Count - 1; i++)
        {
            isSorted = false;
            
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                if (list[j] > list[j + 1])
                {
                    SwapInList(list, j, j + 1);
                    isSorted = true;
                }
            }

            if (!isSorted) return;
        }
    }

    // 삽입 정렬
    static void InsertionSort(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            int target = list[i];  // 현재 값
            int j = i - 1;

            while (j >= 0 && list[j] > target)  // target보다 작은 값들을 한 칸씩 뒤로 밀기
            {
                list[j + 1] = list[j];
                j--;
            }
            
            list[j + 1] = target;   // target을 올바른 위치에 삽입
        }
    }

    // 선택 정렬
    static void SelectionSort(List<int> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            foreach (int l in list)
            {
                Console.Write($"[{l}]");
            }
            
            int minIndex = i;

            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[j] < list[minIndex])
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                SwapInList(list, i, minIndex);
            }
        }
    }

    static void SwapInList(List<int> list, int a, int b)
    {
        int temp = list[a];
        list[a] = list[b];
        list[b] = temp;
    }
}
    
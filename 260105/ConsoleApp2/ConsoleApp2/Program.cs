namespace ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        List<int> list = new List<int>();

        list.Add(5);
        list.Add(2);
        list.Add(8);
        list.Add(3);
        list.Add(1);
        list.Add(6);
        list.Add(4);
        list.Add(7);

        PrintList(list);
        Console.WriteLine();

        MergeSort(list, 0, list.Count - 1);

        PrintList(list);
    }

    static void PrintList(List<int> list)
    {
        foreach (int i in list)
        {
            Console.Write($"[{i}]");
        }
    }

    static void MergeSort(List<int> list, int left, int right)
    {
        if (left >= right) return;
        int center = (left + right) / 2;

        MergeSort(list, left, center);
        MergeSort(list, center + 1, right);

        Merge(list, left, center, right);
    }

    static void Merge(List<int> list, int left, int center, int right)
    {
        // 임시 배열의 크기 = 현재 병합하는 범위의 길이
        int[] temp = new int[right - left + 1];

        int leftIndex = left;
        int rightIndex = center + 1;
        int tempIndex = 0; // temp에서 사용할 인덱스

        // 두 범위에서 남아있는 동안 계속 비교하고, 작은 값을 temp로 이동
        while (leftIndex <= center && rightIndex <= right)
        {
            // 불안정 정렬 [2a][1][2b][3] => [1][2b][2a][3]
            // 안정 정렬 [2a][1][2b][3] => [1][2a][2b][3]

            if (list[leftIndex] <= list[rightIndex]) // 왼쪽을 우선시 함
            {
                temp[tempIndex] = list[leftIndex];
                leftIndex++;
            }
            else
            {
                temp[tempIndex] = list[rightIndex];
                rightIndex++;
            }

            tempIndex++;
        }

        // 왼쪽 범위에 남은 값이 있다면? => 그대로 temp로.
        while (leftIndex <= center)
        {
            temp[tempIndex] = list[leftIndex];
            leftIndex++;
            tempIndex++;
        }

        // 오른쪽 범위에 남은 값이 있다면 => 그대로 temp로.
        while (rightIndex <= right)
        {
            temp[tempIndex] = list[rightIndex];
            rightIndex++;
            tempIndex++;
        }

        // temp배열의 원소만 기존의 List에 덮어쓰기
        for (int t = 0; t < temp.Length; t++)
        {
            list[left + t] = temp[t];
        }
    }
}
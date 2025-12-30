namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        Dictionary<string, int> dict = new();
        
        dict.Add("이승열", 59);
        dict.Add("이인", 19);

        dict.TryAdd("이인", 59);
        // TryAdd로 굳이 같은 키 값을 지닌 것을 추가하는 것 권장하지 않음

        dict["최원탁"] = 12;

        Console.WriteLine(dict["최원탁"]);

        if (!dict.ContainsKey("송근형"))
        {
            dict.Add("송근형", 92);
        }
        else
        {
            // 추가할 수 없었을 때의 처리
        }

        // dict.Remove("이인");

        Console.WriteLine(dict["이인"]);

        foreach (KeyValuePair<string, int> item in dict)
        {
            Console.WriteLine($"key = {item.Key}, value = {item.Value}");
        }

        Console.WriteLine(dict.Count);
        
        
        // 키 값을 int로 할 경우는 List가 더 어울린다. 단, 목적성이 어울리면 사용해도 되나 이 자료구조를 왜 사용했는지 설명하면 된다.
        Dictionary<int, Item> monsters = new();
        List<Item> list = new();
    }
}

public class Item
{
    
}
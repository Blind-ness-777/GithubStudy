namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        
        /*
        int answer = 0;
        int n = Convert.ToInt32(Console.ReadLine());
        
        if (n % 2 == 0)
        {
            for (int i = n; i > 0; i -= 2)
            {
                answer += i * i;
            }
        }
        else
        {
            for (int i = n; i > 0; i -= 2)
            {
                answer += i;
            }
        }
        
        return answer;
        */
    }
    
    public string[] solution(string[] strArr)
    {
        for (int i = 0; i < strArr.Length; i++)
        {
            if (i % 2 == 0) strArr[i] = strArr[i].ToLower();
            else strArr[i] = strArr[i].ToUpper();
        }
        
        return strArr;
    }
}
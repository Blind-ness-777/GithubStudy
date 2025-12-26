namespace ConsoleApp1;

class Program
{
    static int Main(string[] args)
    {
        Console.Clear();
        
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
    }
}
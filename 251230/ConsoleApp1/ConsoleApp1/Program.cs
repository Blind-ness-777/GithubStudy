namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        MatrixGraph<string> graph = new(4);
        
        graph.AddEdgeCycle(0, 1, 1);
        graph.AddEdgeCycle(0, 2, 1);
        graph.AddEdgeCycle(1, 2, 1);
        graph.AddEdgeCycle(1, 3, 1);
        graph.AddEdgeCycle(2, 3, 1);
    }
}

public class ListGraph
{
    private List<int>[] _arr;

    public void AddEdge(int from, int to)
    {
        AddUnique(_arr[from], to);
    }

    public void AddUnique(List<int> list, int value)
    {
        if (!list.Contains(value))
        {
            list.Add(value);
        }
    }
}


public class MatrixGraph<T>
{
    private int[,] _matrix;

    public MatrixGraph(int size)
    {
        _matrix = new int[size, size];
    }
    
    public void AddEdge(int from, int to, int value)
    {
        if (value <= 0) return;
        
        _matrix[from, to] = value;
    }
    
    private void AddEdgeCycle(int a, int b, int value)
    {
        if (!value <= 0)
        {
            
        }
    }
}
namespace ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        MatrixGraph graph = new MatrixGraph(15);
        
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);
        graph.AddEdge(3, 7);
        graph.AddEdge(3, 8);
        graph.AddEdge(4, 9);
        graph.AddEdge(4, 10);
        graph.AddEdge(2, 5);
        graph.AddEdge(2, 6);
        graph.AddEdge(5, 11);
        graph.AddEdge(5, 12);
        graph.AddEdge(6, 13);
        graph.AddEdge(6, 14);
        
        // List<int> BFSPath = graph.BFS(0);
        List<int> stackDFSPath = graph.DFS(0);

        // List<int> DFSPath = graph.DFS(0);
        
        PrintAll(stackDFSPath);
    }

    static void PrintAll(List<int> list)
    {
        foreach (int i in list)
        {
            Console.Write($"[{i}]");
        }
    }
}



public class MatrixGraph
{
    private readonly bool[,] _matrix;

    private int _vertextCount;

    public MatrixGraph(int vertextCount)
    {
        _vertextCount = vertextCount;
        _matrix = new bool[vertextCount, vertextCount];
    }

    public void AddEdge(int from, int to)
    {
        _matrix[from, to] = true;
        _matrix[to, from] = true;
    }

    public bool HasEdge(int from, int to) => _matrix[from, to];

    public List<int> BFS(int start)
    {
        List<int> path = new List<int>();
        Queue<int> queue = new Queue<int>();
        
        bool[] visited = new bool[_vertextCount];
        visited[start] = true;
        
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            path.Add(current);

            for (int to = 0; to < _vertextCount; to++)
            {
                if (_matrix[current, to] && !visited[to])
                {
                    visited[to] = true;
                    queue.Enqueue(to);
                }
            }
        }
        
        return path;
    }

    public List<int> DFS(int start)
    {
        List<int> path = new List<int>();
        bool[] visited = new bool[_vertextCount];
        
        InternalDFS(start, visited, path);
        
        return path;
    }

    public void InternalDFS(int current, bool[] visited, List<int> path)
    {
        visited[current] = true;
        path.Add(current);

        for (int to = 0; to < _vertextCount; to++)
        {
            if (_matrix[current, to] && !visited[to])
            {
                InternalDFS(to, visited, path);
            }
        }
    }
    
    public List<int> StackDFS(int start)
    {
        List<int> path = new List<int>();
        Stack<int> stack = new Stack<int>();
        
        bool[] visited = new bool[_vertextCount];
        visited[start] = true;
        
        stack.Push(start);

        while (stack.Count > 0)
        {
            int current = stack.Pop();
            path.Add(current);

            for (int to = 0; to < _vertextCount; to++)
            {
                if (_matrix[current, to] && !visited[to])
                {
                    visited[to] = true;
                    stack.Push(to);
                }
            }
        }
        
        return path;
    }

}
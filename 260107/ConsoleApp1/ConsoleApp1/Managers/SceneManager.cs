public static class SceneManager
{
    public static Action OnChangeScene;
    public static Scene Current { get; private set; }
    
    private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

    public static void AddScene(string key, Scene state)
    {
        if (_scenes.ContainsKey(key)) return;
        _scenes.Add(key, state);
    }
    
    public static void Change(string key)
    {
        if(!_scenes.ContainsKey(key)) return;
        
        Scene next = _scenes[key];
        
        if (Current == next) return;
        
        Current?.Exit();
        next.Enter();
        
        Current = next;
        
        OnChangeScene?.Invoke();
    }

    public static void Update()
    {
        Current?.Update();
    }

    public static void Render()
    {
        Current?.Render();
    }
}
public class PlayerCharacter : GameObject
{
    // IInteractable
    // Item : GameObject
    // Potion : Item, IInteractable
    
    public PlayerCharacter() => Init();
    public Tile[,] Field { get; set; }
    
    public void Init()
    {
        Symbol = 'P';
    }
    
    public void Update()
    {
        Vector direction = new Vector();
        
        if (InputManager.GetKey(ConsoleKey.UpArrow))
            direction = Vector.Up;

        if (InputManager.GetKey(ConsoleKey.DownArrow))
            direction = Vector.Down;
        
        if (InputManager.GetKey(ConsoleKey.LeftArrow))
            direction = Vector.Left;
        
        if (InputManager.GetKey(ConsoleKey.RightArrow))
            direction = Vector.Right;
        
        if (Field != null) Move(direction);
    }
    
    private void Move(Vector direction)
    {
        Vector current = Position;
        Vector nextPos = Position + direction;  
        
        // 예외 처리
        // 1. 맵 바깝 여부
        // 2. 벽 여부

        Field[Position.Y, Position.X].OnTileObject = null;
        Field[nextPos.Y, nextPos.X].OnTileObject = this;
        Position = nextPos;
        
        Debug.LogWarning($"플레이어 이동 : ({current.X}, {current.Y}) -> ({nextPos.X}, {nextPos.Y})");
    }
}
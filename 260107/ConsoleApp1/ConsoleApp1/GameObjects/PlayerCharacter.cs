public class PlayerCharacter : GameObject
{
    // IInteractable
    // Item : GameObject
    // Potion : Item, IInteractable
    
    public Tile[,] Field { get; set; }
    private Inventory _inventory = new Inventory();
    public bool IsActiveConrtol { get; private set; }
    
    public PlayerCharacter() => Init();
    
    public void Init()
    {
        Symbol = 'P';
    }
    
    public void Update()
    {
        Vector direction = new Vector();

        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            Move(Vector.Up);
            
            IsActiveConrtol = !IsActiveConrtol;
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            Move(Vector.Down);
            
            IsActiveConrtol = !IsActiveConrtol;
        }

        if (InputManager.GetKey(ConsoleKey.LeftArrow))
        {
            Move(Vector.Left);
            
            IsActiveConrtol = !IsActiveConrtol;
            _inventory.SelectLeft();
        }

        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            Move(Vector.Right);
            
            IsActiveConrtol = !IsActiveConrtol;
            _inventory.SelectRight();
        }

        if (InputManager.GetKey(ConsoleKey.B))
        {
            HandleControl();
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _inventory.Select();
        }
    }

    public void HandleControl()
    {
        _inventory.IsActive = !_inventory.IsActive;
        IsActiveConrtol = !IsActiveConrtol;
    }
    
    private void Move(Vector direction)
    {
        if (Field == null || !IsActiveConrtol) return;
        
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

    public void Render()
    {
        _inventory.Render();
    }

    public void AddItem(Item item)
    {
        _inventory.TryAdd(item);
    }
}
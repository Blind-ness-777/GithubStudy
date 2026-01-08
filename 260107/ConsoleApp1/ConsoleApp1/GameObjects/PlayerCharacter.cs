public class PlayerCharacter : GameObject
{
    // IInteractable
    // Item : GameObject
    // Potion : Item, IInteractable
    
    public ObservableProperty<int> Health = new ObservableProperty<int>(100);
    
    public Tile[,] Field { get; set; }
    private Inventory _inventory = new Inventory();
    public bool IsActiveConrtol { get; private set; }
    
    public PlayerCharacter() => Init();
    
    public void Init()
    {
        Symbol = 'P';
        IsActiveConrtol = true;
        Health.AddListener(SetHealthGauge);
        _healthGauge = "■■■■■";
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
    }

    public void Render()
    {
        _inventory.Render();
    }

    public void AddItem(Item item)
    {
        _inventory.TryAdd(item);
    }

    private string _healthGauge;

    public void DrawHealthGauge()
    {
        Console.SetCursorPosition(Position.X - 2, Position.Y - 1);
        _healthGauge.Print(ConsoleColor.Red);
    }
    
    public void SetHealthGauge(int health)
    {
        switch (health)
        {
            case 5:
                _healthGauge = "■■■■■";
                break;
            case 4:
                _healthGauge = "■■■■□";
                break;
            case 3:
                _healthGauge = "■■■□□";
                break;
            case 2:
                _healthGauge = "■■□□□";
                break;
            case 1:
                _healthGauge = "■□□□□";
                break;
        }
    }
}
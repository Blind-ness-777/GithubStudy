using System.Text;

namespace ConsoleApp1;

class Program
{
    private const char PLAYER = 'P';
    private const char PLAYER_ON_GOAL = '@';
    private const char BOMB = 'B';
    private const char BOMB_ON_GOAL = '!';
    private const char GOAL = 'G';
    private const char WALL = '#';
    private const char EMPTY = ' ';

    private static char[,] stageOnemap = new char[,]
    {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', 'B', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', 'P', 'G', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', 'B', ' ', ' ', 'G', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
    };

    private static char[,] stageTwomap = new char[,]
    {
        { '#', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ' },
        { '#', ' ', ' ', ' ', '#', 'G', '#', ' ', ' ', ' ' },
        { '#', ' ', 'B', ' ', '#', ' ', '#', '#', ' ', ' ' },
        { '#', ' ', ' ', 'B', '#', ' ', 'G', '#', ' ', ' ' },
        { '#', ' ', 'P', ' ', ' ', ' ', ' ', '#', ' ', ' ' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ' },
        { '#', '#', ' ', ' ', '#', ' ', '#', '#', ' ', ' ' },
        { ' ', '#', 'B', ' ', '#', ' ', '#', ' ', ' ', ' ' },
        { ' ', '#', ' ', ' ', '#', 'G', '#', ' ', ' ', ' ' },
        { ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ' }
    };

    private static char[,] stageThreemap = new char[,]
    {
        { ' ', ' ', ' ', ' ', '#', '#', '#', ' ', ' ', ' ' },
        { '#', '#', '#', '#', '#', 'G', '#', ' ', ' ', ' ' },
        { '#', ' ', ' ', ' ', '#', ' ', '#', '#', '#', ' ' },
        { '#', ' ', 'B', ' ', '#', ' ', 'G', ' ', '#', ' ' },
        { '#', ' ', 'P', 'B', '#', ' ', ' ', ' ', '#', ' ' },
        { '#', ' ', ' ', ' ', 'B', ' ', ' ', ' ', '#', ' ' },
        { '#', '#', ' ', ' ', '#', ' ', ' ', 'G', '#', ' ' },
        { ' ', '#', 'B', ' ', '#', ' ', '#', '#', '#', ' ' },
        { ' ', '#', ' ', ' ', '#', 'G', '#', ' ', ' ', ' ' },
        { ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', ' ' }
    };
    
    private static char[,] stageFourmap = new char[,]
    {
        { '#', '#', '#', '#', '#', ' ', '#', '#', '#', '#' },
        { '#', 'P', ' ', ' ', '#', ' ', '#', 'G', 'G', '#' },
        { '#', 'B', ' ', 'B', '#', ' ', '#', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', '#', '#', '#', ' ', ' ', '#' },
        { '#', 'B', ' ', 'B', '#', ' ', ' ', ' ', 'G', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', '#', '#', '#', '#', ' ', '#', ' ', '#', '#' },
        { ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#' },
        { ' ', ' ', ' ', ' ', '#', 'G', '#', ' ', 'G', '#' },
        { ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', '#' }
    };
    
    static GameState _gameState = GameState.Menu;
    
    static int _menuIndex = 0;
    
    static string[] _menus = { "START", "EXIT" };
    
    enum GameState
    {
        Menu,
        Playing,
        Exit
    }
    
    static char[,] map;
    static char[,] baseMap;

    static int currentStage = 0;

    static char[][,] stages =
    {
        stageOnemap,
        stageTwomap,
        stageThreemap,
        stageFourmap
    };

    static Position[] startPositions =
    {
        new Position { X = 4, Y = 4 },
        new Position { X = 2, Y = 4 },
        new Position { X = 2, Y = 4 },
        new Position { X = 1, Y = 1 }
    };
    
    static Position _playerPos = new Position()
    {
        X = 4,
        Y = 4
    };
    
    private static int _moveCount = 0;
    
    static int _totalMoveCount = 0;
    
    
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (_gameState != GameState.Exit)
        {
            switch (_gameState)
            {
                case GameState.Menu:
                    ShowMainMenu();
                    break;

                case GameState.Playing:
                    RunGame();
                    break;
            }
        }
        Console.WriteLine("게임 끝");
    }
    
    static void PrintGuideText()
    {
        Console.Clear();
        Console.WriteLine("W : 위로 / S : 아래로 / A : 왼쪽 / D : 오른쪽 / Q : 종료 / R : 다시하기");
        Console.WriteLine("모든 폭탄을 목표지점으로 옮기세요");
        Console.WriteLine();
    }
    
    static void SaveBaseMap()
    {
        baseMap = (char[,])map.Clone();
    }
    
    static void LoadStage(int stageIndex)
    {
        map = (char[,])stages[stageIndex].Clone();
        baseMap = (char[,])stages[stageIndex].Clone();

        _playerPos = startPositions[stageIndex];
        _moveCount = 0;
    }
    
    static void ResetStage()
    {
        map = (char[,])baseMap.Clone();
        _playerPos = startPositions[currentStage];
        _moveCount = 0;
    }
    
    static void LoadNextStage()
    {
        currentStage++;

        if (currentStage >= stages.Length)
        {
            Console.Clear();
            Console.WriteLine("모든 스테이지를 클리어했습니다!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        LoadStage(currentStage);
    }
    
    static bool IsGameClear()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == BOMB || map[y, x] == GOAL)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
    
    static void PrintMoveCount()
    {
        Console.SetCursorPosition(0 , 4);
        Console.Write(new string(' ', 30));
        Console.SetCursorPosition(0, 4);
        Console.WriteLine($"이동 거리 : {_moveCount}");
        Console.WriteLine();
    }
    
    static void PrintClearText()
    {
        Console.WriteLine();
        Console.WriteLine("축하합니다. 클리어 하셨습니다.");
        Console.WriteLine($"현재 스테이지 이동 : {_moveCount}");
        Console.WriteLine($"총 이동 거리 : {_totalMoveCount}");
        Console.WriteLine();
    }
    
    static bool TryGetInput(out ConsoleKey inputKey)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        inputKey = keyInfo.Key;
        
        return inputKey == ConsoleKey.W ||
               inputKey == ConsoleKey.A ||
               inputKey == ConsoleKey.S ||
               inputKey == ConsoleKey.D ||
               inputKey == ConsoleKey.Q ||
               inputKey == ConsoleKey.R;
    }
    static Position GetNextPosition(ConsoleKey inputKey)
    {
        int newX = _playerPos.X;
        int newY = _playerPos.Y;

        if (inputKey == ConsoleKey.W) newY--;
        else if (inputKey == ConsoleKey.S) newY++;
        else if (inputKey == ConsoleKey.A) newX--;
        else if (inputKey == ConsoleKey.D) newX++;

        return new Position()
        {
            X = newX,
            Y = newY
        };
    }
    
    static char GetTile(Position pos)
    {
        return map[pos.Y, pos.X];
    }
    
    static void SetTile(Position pos, char tile)
    {
        map[pos.Y, pos.X] = tile;
    }
    
    static bool IsOutOfArray(Position pos)
    {
        bool outX = pos.X < 0 || map.GetLength(1) <= pos.X;
        bool outY = pos.Y < 0 || map.GetLength(0) <= pos.Y;
        
        return outX || outY;
    }
    
    static void Move(Position from, Position to, char target)
    {
        char originalTile = GetOriginTile(GetTile(from));
        SetTile(from, originalTile);
        
        char targetTile = GetTile(to);
        char nextTile = GetConvertTile(target, targetTile); 
        SetTile(to, nextTile);
    }
    
    static char GetConvertTile(char mover, char under)
    {
        if (mover == PLAYER)
        {
            if (under == GOAL)
            {
                return PLAYER_ON_GOAL;
            }
            else
            {
                return PLAYER;
            }
        }
        else if (mover == BOMB)
        {
            if (under == GOAL)
            {
                return BOMB_ON_GOAL;
            }
            else
            {
                return BOMB;
            }
        }
        
        return under;
    }
    
    static char GetOriginTile(char tile)
    {
        return tile switch
        {
            PLAYER => EMPTY,
            PLAYER_ON_GOAL => GOAL,
            BOMB => EMPTY,
            BOMB_ON_GOAL => GOAL,
            _ => tile
        };
    }
    
    static bool TryPushBomb(Position bombPos)
    {
        Position direction = GetDirection(_playerPos, bombPos);
        Position nextPos = AddDirection(bombPos, direction);
        
        if (IsOutOfArray(nextPos)) return false;
        char nextTile = GetTile(nextPos);
        if (!(nextTile == EMPTY || nextTile == GOAL))  return false;
        
        Move(bombPos, nextPos, BOMB);
        
        Move(_playerPos, bombPos, PLAYER);
        
        return true;
    }

    static Position GetDirection(Position from, Position to)
    {
        return new Position()
        {
            X = to.X - from.X,
            Y = to.Y - from.Y
        };
    }

    static Position AddDirection(Position pos, Position direction)
    {
        return new Position()
        {
            X = pos.X + direction.X,
            Y = pos.Y + direction.Y
        };
    }
    
    static void PrintMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                char tile = map[i, j];
                
                if (tile == WALL) Console.Write("🟦");
                else if(tile == PLAYER) Console.Write("🤖");
                else if(tile == PLAYER_ON_GOAL) Console.Write("✨");
                else if(tile == BOMB) Console.Write("🟫");
                else if(tile == BOMB_ON_GOAL) Console.Write("💎");
                else if(tile == GOAL) Console.Write("🌟");
                else Console.Write("  ");
            }
            Console.WriteLine();
        }
    }
    
    static void PrintMenu()
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (i == _menuIndex)
                Console.WriteLine($"> {_menus[i]}");
            else
                Console.WriteLine($"  {_menus[i]}");
        }
    }
    
    static void PrintLogo()
    {
        Console.Clear();
        Console.WriteLine("███████╗ ██████╗ ██╗  ██╗ ██████╗ ██████╗  █████╗ ███╗   ██╗");
        Console.WriteLine("██╔════╝██╔═══██╗██║ ██╔╝██╔═══██╗██╔══██╗██╔══██╗████╗  ██║");
        Console.WriteLine("███████╗██║   ██║█████╔╝ ██║   ██║██████╔╝███████║██╔██╗ ██║");
        Console.WriteLine("╚════██║██║   ██║██╔═██╗ ██║   ██║██╔══██╗██╔══██║██║╚██╗██║");
        Console.WriteLine("███████║╚██████╔╝██║  ██╗╚██████╔╝██║  ██║██║  ██║██║ ╚████║");
        Console.WriteLine("╚══════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝");
        Console.WriteLine();
    }
    
    static void ShowMainMenu()
    {
        PrintLogo();
        PrintMenu();
        
        ConsoleKey key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.W)
            _menuIndex = (_menuIndex - 1 + _menus.Length) % _menus.Length;
        else if (key == ConsoleKey.S)
            _menuIndex = (_menuIndex + 1) % _menus.Length;
        else if (key == ConsoleKey.Enter)
        {
            if (_menuIndex == 0) // START
            {
                _gameState = GameState.Playing;
                Console.Clear();
                PrintGuideText();
                LoadStage(0);
            }
            else // EXIT
            {
                _gameState = GameState.Exit;
            }
        }
    }
    
    static void RunGame()
    {
        while (true)
        {
            PrintMoveCount();
            PrintMap();

            if (IsGameClear())
            {
                PrintClearText();
                LoadNextStage();
                return;
            }

            ConsoleKey inputKey;
            
            if (!TryGetInput(out inputKey)) continue;

            if (inputKey == ConsoleKey.Q)
            {
                Console.WriteLine("\n게임을 종료합니다.");
                return;
            }

            if (inputKey == ConsoleKey.R)
            {
                ResetStage();
                continue;
            }
            
            Position nextPos = GetNextPosition(inputKey);

            if (IsOutOfArray(nextPos)) continue;
            
            char targetTile = GetTile(nextPos);
            if (targetTile == WALL) continue;

            if (targetTile == EMPTY || targetTile == GOAL)
            {
                Move(_playerPos, nextPos, PLAYER);
                _playerPos = nextPos;
                _moveCount++;
                _totalMoveCount++;
            }
            else if (targetTile == BOMB || targetTile == BOMB_ON_GOAL)
            {
                if (TryPushBomb(nextPos))
                {
                    _playerPos = nextPos;
                    _moveCount++;
                    _totalMoveCount++;
                }
            }
        }
    }
}

public struct Position()
{
    public int X;
    public int Y;
}

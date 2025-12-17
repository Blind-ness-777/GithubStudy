using System.Text;

namespace ConsoleProject;

// 소코반 게임 만들기(콘솔)
// 절차지향 - 위에서 아래로 절차적으로 수행하도록 만드는 구조
// 함수를 적극 사용하자.
        
// 게임로직
    // 1. 사용자 입력
    // 2. 로직 수행
    // 3. 화면에 출력
            
// 소코반 구현 구조
    // 1. 초기 세팅
    // 2.
    // 3. 사용자 입력
    // 4. 로직 수행(이동, 폭탄 밀기)
    // 5. 화면에 출력
    // 6. 2~4 반복

class Program
{
    private const char PLAYER = 'P';            // 플레이어
    private const char PLAYER_ON_GOAL = '@';    // 플레이어가 목표지점 위에 있는 상태
    private const char BOMB = 'B';              // 폭탄
    private const char BOMB_ON_GOAL = '!';      // 폭탄이 목표지점 위에 있는 상태
    private const char GOAL = 'G';              // 목표지점
    private const char WALL = '#';              // 벽
    private const char EMPTY = ' ';             // 빈공간

    private static char[,] stageOnemap = new char[,] // 게임 필드(문자 기반 2차원 배열)
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
    
    static char[,] map;        // 현재 플레이 중인 맵
    static char[,] baseMap;   // 리셋용 베이스 맵

    static int currentStage = 0;

    static char[][,] stages =
    {
        stageOnemap,
        stageTwomap,
        stageThreemap
    };

    static Position[] startPositions =
    {
        new Position { X = 4, Y = 4 },
        new Position { X = 2, Y = 4 },
        new Position { X = 2, Y = 4 }
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
        
        // 안내 멘트 출력
        PrintGuideText();
        
        LoadStage(0);

        while (true)
        {
            // 출력
            PrintMoveCount();
            PrintMap();
            
            // 클리어 시 반복 종료
            if (IsGameClear())
            {
                PrintClearText();
                LoadNextStage();
                continue;
            }
            
            // 사용자 입력
            ConsoleKey inputKey;
            if(!TryGetInput(out inputKey)) continue;
            
            // 종료 처리
            if (inputKey == ConsoleKey.Q)
            {
                Console.WriteLine("\n게임을 종료합니다");
                break;
            }

            if (inputKey == ConsoleKey.R)
            {
                ResetStage();
                continue;
            }
            
            // 로직 수행
            
            // 이동 가능한지 판단.
            Position nextPos = GetNextPosition(inputKey);
            
            if (IsOutOfArray(nextPos)) continue;
            
            char targetTile = GetTile(nextPos);
            if (targetTile == WALL) continue;
            
            // 이동 구현(이동, 폭탄 밀기)
            
            // 플레이어 단순 이동 (Goal 위로 이동하는 것도 포함)
            if (targetTile == EMPTY || targetTile == GOAL)
            {
                Move(_playerPos, nextPos, PLAYER);
                _playerPos = nextPos;
                _moveCount++;
                _totalMoveCount++;
            }
            // 폭탄을 밀면서 이동
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

        Console.WriteLine("게임 끝");
    }

    static void PrintGuideText()
    {
        Console.Clear();
        Console.WriteLine("W : 위로 / S : 아래로 / A : 왼쪽 / D : 오른쪽 / Q : 종료");
        Console.WriteLine("모든 폭탄을 목표지점으로 옮기세요");
        Console.WriteLine();
    }

    static void PrintMoveCount()
    {
        Console.SetCursorPosition(0 , 4);
        Console.Write(new string(' ', 30)); // 이전 출력 지우기
        Console.SetCursorPosition(0, 4);
        Console.WriteLine($"이동 거리 : {_moveCount}");
        Console.WriteLine();
    }
    
    /// <summary>
    /// 현재 진행 중인 맵을 베이스로 삼는 함수
    /// </summary>
    static void SaveBaseMap()
    {
        baseMap = (char[,])map.Clone();
    }
    
    /// <summary>
    /// 선택한 스테이지를 불러와 현재 맵과 리셋용 베이스 맵을 초기화하는 함수
    /// </summary>
    /// <param name="stageIndex">불러올 스테이지 인덱스</param>
    static void LoadStage(int stageIndex)
    {
        map = (char[,])stages[stageIndex].Clone();
        baseMap = (char[,])stages[stageIndex].Clone();

        _playerPos = startPositions[stageIndex];
        _moveCount = 0;
    }
    
    /// <summary>
    /// 다음 스테이지를 불러올 함수
    /// </summary>
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
    
    /// <summary>
    /// 현재 스테이지가 클리어 불가능할 때 리셋하는 함수
    /// </summary>
    static void ResetStage()
    {
        map = (char[,])baseMap.Clone();
        _playerPos = startPositions[currentStage];
        _moveCount = 0;
    }
    
    /// <summary>
    /// 게임이 클리어 된 상황인지 판단하는 함수
    /// </summary>
    /// <returns>클리어 되었다면 true, 아니라면 false</returns>
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

    static void PrintClearText()
    {
        Console.WriteLine();
        Console.WriteLine("축하합니다. 클리어 하셨습니다.");
        Console.WriteLine($"현재 스테이지 이동 : {_moveCount}");
        Console.WriteLine($"총 이동 거리 : {_totalMoveCount}");
        Console.WriteLine();
    }
    
    /// <summary>
    /// 사용자 입력을 받고, 게임에서 사용하는 키인지 판단
    /// </summary>
    /// <param name="inputKey">입력된 키를 보관할 변수</param>
    /// <returns>게임에서 사용하는 키라면 true, 아니라면 false</returns>
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
    
    
    /// <summary>
    /// 입력받은 키에 따라 다음 좌표정보를 반환하는 함수
    /// </summary>
    /// <param name="inputKey">입력 키</param>
    /// <returns>입력 키에 따른 다음 좌표 구조체</returns>
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
    
    /// <summary>
    /// 입력받은 좌표의 타일을 반환하는 함수
    /// </summary>
    /// <param name="pos">입력 위치</param>
    /// <returns>배열의 char 문자 반환</returns>
    static char GetTile(Position pos)
    {
        return map[pos.Y, pos.X];
    }
    
    /// <summary>
    /// 입력받은 좌표의 타일을 바꾸는 함수
    /// </summary>
    /// <param name="pos">바꿀 위치</param>
    /// <param name="tile">대상 타일</param>
    static void SetTile(Position pos, char tile)
    {
        map[pos.Y, pos.X] = tile;
    }
    
    /// <summary>
    /// 입력받은 위치가 배열의 범위 바깥인지 판단하는 함수
    /// </summary>
    /// <param name="pos">입력 위치</param>
    /// <returns>배열 인덱스 범위 밖이라면 true, 아니라면 false</returns>
    static bool IsOutOfArray(Position pos)
    {
        bool outX = pos.X < 0 || map.GetLength(1) <= pos.X;
        bool outY = pos.Y < 0 || map.GetLength(0) <= pos.Y;
        
        return outX || outY;
    }
    
    /// <summary>
    /// 게임 오브젝트의 위치를 이동시킨다
    /// </summary>
    /// <param name="from">이전 위치</param>
    /// <param name="to">이동 후 위치</param>
    /// <param name="target">이동시킬 대상</param>
    static void Move(Position from, Position to, char target)
    {
        // 출발지점을 기존 타일로 바꿔서 비우기
        char originalTile = GetOriginTile(GetTile(from));
        SetTile(from, originalTile);
        // 다음 위치에 'P'를 넣어야 함.
        char targetTile = GetTile(to);
        char nextTile = GetConvertTile(target, targetTile); 
        SetTile(to, nextTile);
    }
    
    /// <summary>
    /// 이동할 위치에 표시될 타일을 구하는 함수
    /// </summary>
    /// <param name="mover">이동하는 주체</param>
    /// <param name="under">이동할 곳의 타일</param>
    /// <returns>이동 시 바뀌어야 할 타일</returns>
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
    
    /// <summary>
    /// 겹쳐져 있던 원본 타일을 반환하는 함수
    /// </summary>
    /// <param name="tile">변환할 타일</param>
    /// <returns>변환된 타일</returns>
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
    
    /// <summary>
    /// 폭탄을 미는 함수
    /// </summary>
    /// <param name="bombPos">폭탄의 위치</param>
    /// <returns>폭탄을 성골적으로 밀었다면 true, 아니라면 false</returns>
    static bool TryPushBomb(Position bombPos)
    {
        // 방향
        Position direction = GetDirection(_playerPos, bombPos);
        // 구해진 방향으로 한 칸 전진했을 때의 위치
        Position nextPos = AddDirection(bombPos, direction);
        
        // 맵 밖으로 나가는지 확인
        if (IsOutOfArray(nextPos)) return false;
        // 밀릴 수 없는 경우
        char nextTile = GetTile(nextPos);
        if (!(nextTile == EMPTY || nextTile == GOAL))  return false;

        // 폭탄 이동
        Move(bombPos, nextPos, BOMB);
        
        // 플레이어를 폭탄이 있던 위치로 이동
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
    
    /// <summary>
    /// 맵 출력 함수
    /// </summary>
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
}

public struct Position()
{
    public int X;
    public int Y;
}
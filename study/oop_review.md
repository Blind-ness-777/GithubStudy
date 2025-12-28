# C# 기초 문법 & OOP 교과서식 복습 노트 (부품 분해 방식, 확장판 v4)

> 목표: “코드 한 덩어리 → 그 안의 구성요소(키워드/기호/구문/규칙)를 부품 단위로 분해”해서, **왜 이런 문법이 존재하는지**와 **실제로 어떻게 동작하는지**까지 이해한다.  
> 범위: 객체지향(OOP) 기초 + 최근 TIL 주제(연산자, 논리, 반복 제어, 배열 길이, Parse/TryParse, delegate/Invoke, +=/-=, GetLength 등) + v4 추가( switch, return, 함수 시그니처, static 범위, out의 의미, 배열 인덱스 실수 패턴 ).

---

## 0. 프로그램 시작 구조: `Main`

```csharp
class Program
{
    static void Main(string[] args)
    {
        // 실행 시작 지점
    }
}
```

### 부품 분해 (한 줄씩 뜯기)

#### `class Program`
- `class`  
  - “클래스(설계도)를 정의하겠다”라는 선언 키워드.
  - 클래스는 필드/메서드/프로퍼티/생성자 같은 멤버를 담는다.
- `Program`  
  - 클래스 이름(식별자). 파일명과 같을 필요는 없음.

#### `{ ... }` (중괄호 블록)
- `{}`는 “범위(scope)”를 만든다.
- 클래스 블록 안에는 멤버가 들어가고, 메서드 블록 안에는 실행 문장이 들어간다.

#### `static void Main(string[] args)`
- `static`  
  - 객체를 만들지 않아도 접근 가능한 멤버라는 뜻.
  - 프로그램 시작 순간에는 객체가 없으므로 시작점 `Main`은 보통 static이어야 한다.
- `void`  
  - 반환값이 없다.
- `Main`  
  - 런타임이 실행 시작점으로 찾는 특수한 이름.
- `(string[] args)`  
  - 매개변수 목록.
  - `string[]`는 문자열 배열 타입, `args`는 변수명.
  - 명령줄 인자를 받을 때 사용(초심 단계에서는 비워져 있는 경우가 많음).

#### `// ...`
- 한 줄 주석. 실행에 영향 없음.

---

## 1. 타입과 변수: `int`, `double`, `bool`, `char`, `string`

```csharp
int a = 10;
double b = 3.14;
bool ok = true;
char c = 'A';
string s = "Hello";
```

### 부품 분해
- `타입(type)`은 “메모리에 어떤 형태로 저장할지”와 “어떤 연산이 가능한지”를 정한다.
- `a, b, ok, c, s`는 변수명(식별자).
- `=`는 대입(오른쪽 값을 왼쪽에 저장).
- `;`는 문(statement) 종료 표시.
- (참고) 실수 리터럴 `3.14`는 기본이 `double`로 취급되는 경우가 많고, `float`는 `3.14f`처럼 접미사를 붙여 구분하는 경우가 있다.

---

## 2. 연산자(Operator)와 축약 대입: `op=`

```csharp
int x = 5;
x += 3;  // x = x + 3
x -= 2;  // x = x - 2
x *= 4;  // x = x * 4
x /= 2;  // x = x / 2
```

### 부품 분해
- `+=`는 “더해서 대입” 축약형이며 `x = x + 3`과 같은 의미.
- `-=` `*=` `/=`도 같은 패턴.
- 포인트: 축약은 단지 “짧게 쓰는 문법”이 아니라, **상태를 갱신하는 패턴**을 자주 쓰기 때문에 존재한다.

---

## 3. 비교/논리 연산: `==`, `!=`, `<`, `<=`, `&&`, `||`, `!`

```csharp
int hp = 10;

if (hp <= 0 && hp != -1)
{
    // ...
}
```

### 부품 분해
- 비교 연산의 결과는 항상 `bool`이다.
- `&&`/`||`는 **단락 평가(short-circuit)**가 될 수 있다.
  - `A && B`: A가 false면 B는 평가하지 않을 수 있다.
  - `A || B`: A가 true면 B는 평가하지 않을 수 있다.
- 단락 평가가 중요한 이유: 오른쪽에 “예외 날 수 있는 코드”가 있어도 왼쪽에서 결론이 나면 안전해질 수 있다.

---

## 4. 조건문: `if / else if / else`

```csharp
if (score >= 90)
{
    // A
}
else if (score >= 80)
{
    // B
}
else
{
    // C
}
```

### 부품 분해
- `if`는 조건이 true일 때 실행.
- `else if`는 이전 조건이 false일 때만 검사.
- `else`는 위가 모두 false일 때 실행하는 마지막 분기.

---

## 5. 조건 분기: `switch` (v4 추가)

`switch`는 “값이 무엇이냐”에 따라 분기할 때 사용한다.

```csharp
int menu = 2;

switch (menu)
{
    case 1:
        Console.WriteLine("시작");
        break;

    case 2:
        Console.WriteLine("불러오기");
        break;

    default:
        Console.WriteLine("잘못된 입력");
        break;
}
```

### 부품 분해
- `switch (menu)`  
  - 괄호 안의 값을 기준으로 아래 `case` 중 하나를 고른다.
- `case 1:`  
  - menu가 1이면 여기부터 실행.
- `break;`  
  - switch를 빠져나간다.
  - (중요) break가 없으면 다음 case까지 이어서 실행되는 형태가 생길 수 있어 초심자에겐 위험하다(“fall-through”를 막는 용도로 이해).
- `default:`  
  - 어느 case에도 해당하지 않을 때 실행되는 “기본값 처리”.

**언제 쓰면 좋나?**  
- “메뉴 선택”, “상태(State)별 처리”, “명령어 처리”처럼 값이 딱딱 떨어질 때.

---

## 6. 반복문: `while`, `for`

### 6-1. while

```csharp
int i = 0;
while (i < 3)
{
    i++;
}
```

- 종료 조건을 스스로 만들어야 한다(안 그러면 무한 루프 위험).

### 6-2. for

```csharp
for (int i = 0; i < 3; i++)
{
}
```

- `for (초기식; 조건식; 증감식)` 구조로 “반복 횟수가 명확”할 때 많이 사용.

---

## 7. 반복 제어: `break`, `continue`

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 3) continue;
    if (i == 7) break;

    Console.WriteLine(i);
}
```

- `continue`: 이번 회차의 남은 코드를 건너뛰고 다음 반복으로.
- `break`: 반복문 자체를 즉시 종료.

---

## 8. 배열(Array)과 길이: `Length`, `GetLength`

### 8-1. 1차원 배열

```csharp
int[] arr = new int[5];
int n = arr.Length;

arr[0] = 10;
arr[4] = 99;
```

### 부품 분해
- `int[]` : int 배열 타입.
- `new int[5]` : 길이 5짜리 배열 생성.
- `Length` : 총 칸 수.
- 인덱스는 0부터 시작.
  - 첫 칸: `arr[0]`
  - 마지막 칸: `arr[arr.Length - 1]`

### 8-2. 2차원 배열

```csharp
int[,] map = new int[3, 4];

int rows = map.GetLength(0);
int cols = map.GetLength(1);
```

- `GetLength(0)`은 첫 번째 차원의 길이(보통 행), `GetLength(1)`은 두 번째 차원의 길이(보통 열).

---

## 9. 배열 인덱스 실수(Off-by-one) 패턴 (v4 추가)

배열에서 제일 흔한 실수는 “마지막 인덱스를 착각”하는 것이다.

### 9-1. 흔한 실수 1: `<= Length`

```csharp
for (int i = 0; i <= arr.Length; i++) // ❌ 위험
{
    Console.WriteLine(arr[i]);
}
```

- `arr.Length`는 “칸 수”이고, 마지막 인덱스는 `Length - 1`이다.
- 그래서 조건은 보통 `i < arr.Length`가 안전하다.

### 9-2. 올바른 패턴

```csharp
for (int i = 0; i < arr.Length; i++) // ✅
{
    Console.WriteLine(arr[i]);
}
```

### 9-3. 흔한 실수 2: 빈 배열/빈 칸 처리
- 인벤토리 6칸 같은 걸 만들 때, “비어있음”을 표시하는 규칙을 정하지 않으면 로직이 꼬인다.
- 초심 단계에서는 보통:
  - 값 타입 배열이면 “0을 비어있음으로 쓰기” 같은 약속을 잡거나
  - (수업 범위에 따라) 별도의 bool 배열로 “사용중인지”를 관리하기도 한다.

---

## 10. enum(열거형)

```csharp
enum State
{
    Idle,
    Battle,
    Shop
}
```

- enum은 “가능한 값의 목록”을 타입으로 만든 것.
- 숫자보다 이름으로 상태를 표현하므로 코드 읽기가 쉬워진다.

---

## 11. OOP 핵심 1: 클래스/객체/멤버(필드/메서드)

```csharp
class Player
{
    public string Name;
    public int Hp;

    public void Heal(int amount)
    {
        Hp += amount;
    }
}
```

### 부품 분해
- 필드(field): 데이터를 저장하는 변수.
- 메서드(method): 행동(로직)을 담는 함수.
- 객체 생성:
  ```csharp
  Player p = new Player();
  p.Heal(10);
  ```
- `.`은 멤버 접근 연산자.

---

## 12. 함수 시그니처(메서드 모양) 이해하기 (v4 추가)

메서드는 “이름 + 매개변수 + 반환값”의 조합으로 구분된다.

```csharp
public int Add(int a, int b)
{
    return a + b;
}
```

### 부품 분해
- `public` : 외부에서 호출 가능.
- `int` : 반환 타입(이 함수는 int 값을 돌려준다).
- `Add` : 함수 이름.
- `(int a, int b)` : 매개변수 목록. 호출자가 값을 넣어준다.
- `{ ... }` : 함수 몸통.
- `return a + b;` : 값을 반환하고 함수 종료.

**호출은 이렇게:**
```csharp
int result = Add(2, 3);
```

- `Add(2,3)`의 결과가 int이므로 변수에 받을 수 있다.

---

## 13. `return`의 역할 (v4 추가)

`return`은 두 가지 역할을 한다.

### 13-1. 값을 반환한다
```csharp
int Square(int n)
{
    return n * n;
}
```

### 13-2. 함수를 즉시 종료한다(조기 종료)
```csharp
void PrintIfPositive(int n)
{
    if (n <= 0) return; // 여기서 함수 끝
    Console.WriteLine(n);
}
```

- 반환 타입이 `void`여도 `return;`은 사용할 수 있다(“그냥 끝내기”).

---

## 14. 접근 제한자 `public / private`

```csharp
class Player
{
    private int hp;
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 0) hp = 0;
    }
}
```

- `private`는 외부에서 직접 바꾸지 못하게 막는다(캡슐화).
- `public` 메서드로만 상태를 바꾸게 해서 “규칙을 한 곳에 모으는” 효과가 있다.

---

## 15. 프로퍼티(Property) `get / set`

```csharp
class Player
{
    private int hp;

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
}
```

### 부품 분해(핵심)
- 외부에서 보기엔 `Hp`라는 “값”처럼 보이지만,
- 내부적으로는 “읽기(get) / 쓰기(set)” 동작을 가진 **통로**다.
- `value`는 set 블록에 자동으로 들어오는 “대입된 값”이다.

**예:**
- `int x = p.Hp;` → get 실행
- `p.Hp = 10;` → set 실행

---

## 16. Parse / TryParse (문자열 → 숫자)

### 16-1. Parse
```csharp
int n = int.Parse(text);
```
- 실패하면 예외가 날 수 있다.

### 16-2. TryParse
```csharp
int n;
bool ok = int.TryParse(text, out n);
```
- `ok`가 true면 변환 성공.
- 실패해도 예외 대신 ok=false로 처리 가능.

---

## 17. `out`의 의미 (v4 추가)

`out`은 “함수가 값을 **바깥 변수에 채워 넣는** 약속”이다.

```csharp
int n;
bool ok = int.TryParse("123", out n);
```

### 부품 분해
- `n`은 원래 호출자(바깥쪽) 변수.
- `out n`을 넘기면,
  - TryParse가 내부에서 `n = 123;` 같은 식으로 **값을 채워준다**.
- 그래서 TryParse는 결과가 2개처럼 보인다:
  - 성공/실패 여부(반환값 bool)
  - 변환된 숫자(out로 채워지는 값)

> (메모리 관점 힌트) out은 “그 변수의 칸을 함수에게 맡겨서 채우게 하는 느낌”으로 이해하면 초심 단계에서 충분하다.

---

## 18. `static`의 범위(왜 쓰고, 어디서 문제 생기나) (v4 추가)

### 18-1. static 멤버는 “공용”이다

```csharp
class Counter
{
    public static int Total;
}
```

- `Counter.Total`처럼 **클래스 이름으로 접근**한다.
- 객체마다 따로 생기는 게 아니라 “클래스 전체가 공유하는 값”이다.

### 18-2. 인스턴스 멤버는 “개별”이다

```csharp
class Counter
{
    public int Value;
}
```

- `new Counter()`를 만들 때마다 `Value`가 각 객체에 따로 생긴다.

### 18-3. 흔한 혼동
- `static`은 “편해서” 쓰는 게 아니라 “공유되어야 해서” 쓴다.
- 잘못 남발하면 모든 객체가 같은 값을 공유해 버려서 버그가 생긴다.

---

## 19. 델리게이트(delegate) / `+=` `-=` / `Invoke`

### 19-1. delegate 타입 선언
```csharp
delegate void Simple();
```
- 이 모양(매개변수 없음, void 반환)의 함수만 담을 수 있는 타입.

### 19-2. 델리게이트 변수에 함수 담기
```csharp
static void Hello()
{
    Console.WriteLine("Hello");
}

Simple s = Hello;
```

### 19-3. Invoke(): 담긴 함수 실행
```csharp
s.Invoke();
```

### 19-4. `+=` / `-=` : 함수 목록 추가/제거(멀티캐스트)
```csharp
static void A(){ }
static void B(){ }

Simple s = null;
s += A; // 등록(구독)
s += B; // 등록
s -= A; // 해제
```

- `s.Invoke()`하면 남아있는 함수들이 순서대로 실행될 수 있다.

### 19-5. null 안전 호출
```csharp
s?.Invoke();
```

- s가 null이면 아무것도 하지 않음(예외 방지).

---

## 20. 지금까지 문법을 한 덩어리로 연결하면

- **데이터**: 필드 / 배열 / enum  
- **흐름 제어**: if / switch / for / while / break / continue  
- **입력 안전 처리**: TryParse + out  
- **OOP 구조**: class / public-private / property(get-set)  
- **공유 개념**: static  
- **객체 간 반응(구독/알림)**: delegate / += -= / Invoke  

---

## 부록: 복습 루틴(추천)
1) 각 섹션 코드 블록을 보고 키워드의 역할을 “말로 설명”해보기  
2) 설명을 보고 틀린 부분 체크  
3) 숫자/조건/배열 크기/케이스 값을 살짝 바꿔 직접 실행해보기  
4) 오류가 나면: “어느 부품을 잘못 이해했는지”로 원인을 추적해보기

---

using System;
using System.Collections.Generic;
using System.Text;

public class MenuList
{
    private List<(string text, Action action)> _menus;
    private int _currentIndex;

    public MenuList(params (string, Action)[] menuTexts)
    {
        if (menuTexts.Length == 0)
        {
            _menus = new List<(string, Action)>();
        }
        else
        {
            _menus = menuTexts.ToList();
        }
    }
    
    public void Reset()
    {
        _currentIndex = 0;
    }

    public void Select()
    {
        _menus[_currentIndex].action?.Invoke();
    }
    
    public void Add(string text, Action action)
    {
        _menus.Add((text, action));
    }
    
    public void SelectUp()
    {
        _currentIndex--;

        if (_currentIndex < 0)
            _currentIndex = 0;
    }

    public void SelectDown()
    {
        _currentIndex++;

        if (_currentIndex >= _menus.Count)
            _currentIndex = _menus.Count - 1;
    }
    
    public void Render(int x, int y)
    {
        for (int i = 0; i < _menus.Count; i++)
        {
            y++;
            Console.SetCursorPosition(x, y);
            if (i == _currentIndex)
            {
                "->".Print(ConsoleColor.Green);
                _menus[i].text.Print(ConsoleColor.Green);
                continue;
            }
            else
            {
                Console.Write("  ");
                _menus[i].text.Print();
            }
        }
    }
}

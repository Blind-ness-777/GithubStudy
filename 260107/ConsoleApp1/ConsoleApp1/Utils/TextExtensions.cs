using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

public static class TextExtensions
{
    public static void Print(this string text, ConsoleColor color = ConsoleColor.Gray)
    {
        if (!(color == ConsoleColor.Gray)) Console.ForegroundColor = color;

        Console.Write(text);

        if (!(color == ConsoleColor.Gray)) Console.ResetColor();
    }

    public static void Print(this char character, ConsoleColor color = ConsoleColor.Gray)
    {
        if (!(color == ConsoleColor.Gray)) Console.ForegroundColor = color;

        Console.Write(character);

        if (!(color == ConsoleColor.Gray)) Console.ResetColor();
    }
}


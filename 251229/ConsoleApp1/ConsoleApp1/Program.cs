using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
    }
}

public enum Shape
{
    Spade,
    Heart,
    Clover,
    Diamond
};

public class Card
{
    public Shape CardShape { get; private set; }
    public int Number { get; private set; }
    
    public Card(Shape shape, int value)
    {
        CardShape = shape;
        Number = value;
    }
}

public class CardDeck
{
    private Stack<Card> unusedCards = new Stack<Card>();
    private Stack<Card> usedCards = new Stack<Card>();

    public CardDeck()
    {
        Shape[] shapes = new Shape[]
        {
            Shape.Spade,
            Shape.Heart,
            Shape.Clover,
            Shape.Diamond
        };
        
        foreach (Shape shape in shapes)
        {
            for (int number = 1; number <= 13; number++)
            {
                unusedCards.Push(new Card(shape, number));
            }
        }
    }

    public Card ShowTopCard()
    {
        return unusedCards.Peek();
    }

    public Card DrawCard()
    {
        Card card = unusedCards.Pop();
        usedCards.Push(card);
        return card;
    }
}

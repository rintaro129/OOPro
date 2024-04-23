using System.Drawing;

namespace Game;

public class VisualEntityEventArgs : EventArgs
{
    public char Sprite { get; set; }
    public ConsoleColor Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public VisualEntityEventArgs(BaseEntity entity)
    {
        Sprite = entity.GetSprite();
        Color = entity.GetSpriteColor();
        X = entity.X;
        Y = entity.Y;
    }

    public VisualEntityEventArgs(char sprite, ConsoleColor color, int x, int y)
    {
        Sprite = sprite;
        Color = color;
        X = x;
        Y = y;
    }
    
}
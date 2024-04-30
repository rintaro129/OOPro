using System.Drawing;

namespace BattleCity;

public class VisualEntityEventArgs : EventArgs
{
    public char Sprite { get; set; } = ' ';
    public ConsoleColor Color { get; set; } = ConsoleColor.White;
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public int X { get; set; }
    public int Y { get; set; }

    public VisualEntityEventArgs(BaseEntity entity)
    {
        SetSprite(entity);
        SetColor(entity);
        SetBackgroundColor(entity);
        X = entity.X;
        Y = entity.Y;
    }

    private void SetSprite(BaseEntity entity)
    {
        switch (entity)
        {
            case Tank:
                Sprite = entity.Direction switch
                {
                    Direction.Up => 'Ʌ',
                    Direction.Down => 'V',
                    Direction.Left => '<',
                    Direction.Right => '>',
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            case Bullet:
                Sprite = '·';
                break;
            case BrickWall:
                Sprite = '#';
                break;
            case SteelWall:
                Sprite = '\u2588';
                break;
            case Bomb:
                Sprite = 'o';
                break;
        }
    }

    private void SetColor(BaseEntity entity)
    {
        switch (entity)
        {
            case Player:
                Color = ConsoleColor.Green;
                break;
            case EnemyLvl2:
                Color = ConsoleColor.DarkBlue;
                break;
            case EnemyLvl3:
                Color = ConsoleColor.Red;
                break;
            case BrickWall:
                Color = ConsoleColor.DarkRed;
                break;
            case SteelWall:
                Color = ConsoleColor.Gray;
                break;
            case Bomb:
                Color = ConsoleColor.Black;
                break;
        }
    }

    private void SetBackgroundColor(BaseEntity entity)
    {
        switch (entity)
        {
            case Bomb:
                BackgroundColor = ConsoleColor.Yellow;
                break;
        }
    }

    public VisualEntityEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }
}
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
            case Explosion:
                Sprite = '\u256c';
                break;
            case Spawn spawn:
                switch (spawn.TicksPassed % 4)
                {
                    case 0:
                        Sprite = '-';
                        break;
                    case 1:
                        Sprite = '/';
                        break;
                    case 2:
                        Sprite = '|';
                        break;
                    case 3:
                        Sprite = '\\';
                        break;
                }

                break;
        }
    }

    private void SetColor(BaseEntity entity)
    {
        switch (entity)
        {
            case EnemyLvl3:
                Color = ConsoleColor.Red;
                break;
            case EnemyLvl2:
                Color = ConsoleColor.DarkBlue;
                break;
            case Player:
                Color = ConsoleColor.Green;
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
            case Explosion:
                Color = ConsoleColor.Black;
                break;
            case Spawn:
                Color = ConsoleColor.DarkMagenta;
                break;
        }
    }

    private void SetBackgroundColor(BaseEntity entity)
    {
        switch (entity)
        {
            case Tank tank:
                if (tank.WasDamaged) BackgroundColor = ConsoleColor.Red;
                break;
            case Bomb:
                BackgroundColor = ConsoleColor.Yellow;
                break;
            case Explosion:
                BackgroundColor = ConsoleColor.DarkYellow;
                break;
        }
    }

    public VisualEntityEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }
}
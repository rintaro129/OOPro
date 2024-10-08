using System.Drawing;

namespace BattleCity;

public class IOEventArgs : EventArgs
{
    public char Sprite { get; set; } = ' ';
    public ConsoleColor Color { get; set; } = ConsoleColor.White;
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public int X { get; set; }
    public int Y { get; set; }
    public BaseEntity Entity { get; }
    public const string SPRITES_DIRECTORY = @"res\Sprites\";
    public string FilePath { get; set; }

    public IOEventArgs(BaseEntity entity)
    {
        Entity = entity;
        SetSprite(entity);
        SetColor(entity);
        SetBackgroundColor(entity);
        SetFilePath(entity);
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
                switch (spawn.TurnsPassed % 4)
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
            case PrizeHealth:
                Sprite = '+';
                break;
            case PrizeSpeed:
                Sprite = '~';
                break;
            case PrizeFreeze:
                Sprite = '*';
                break;
        }
    }
    private void SetFilePath(BaseEntity entity)
    {
        string res = SPRITES_DIRECTORY + entity.GetType().Name;
        switch(entity)
        {
            case Tank tank:
                res += tank.Direction.ToString();
                if(tank.WasDamaged)
                {
                    res += "Damaged";
                }
                break;
            case Spawn spawn:
                res += spawn.TurnsPassed % 4;
                break;
        }
        res += ".png";
        FilePath = res;
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
                Color = ConsoleColor.DarkGray;
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
            case PrizeHealth:
                Color = ConsoleColor.Red;
                break;
            case PrizeSpeed:
                Color = ConsoleColor.DarkYellow;
                break;
            case PrizeFreeze:
                Color = ConsoleColor.Blue;
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
            case Prize:
                BackgroundColor = ConsoleColor.White;
                break;
        }
    }

    public IOEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }
}
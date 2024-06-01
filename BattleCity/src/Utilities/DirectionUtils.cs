namespace BattleCity;

public class DirectionUtils
{
    public static Direction ToDirection(int x, int y)
    {
        return x switch
        {
            0 when y == -1 => Direction.Up,
            0 when y == 1 => Direction.Down,
            -1 when y == 0 => Direction.Left,
            1 when y == 0 => Direction.Right,
            _ => throw new ArgumentException()
        };
    }

    public static (int, int) ToInts(Direction direction)
    {
        return direction switch
        {
            Direction.Down => (0, 1),
            Direction.Up => (0, -1),
            Direction.Right => (1, 0),
            Direction.Left => (-1, 0),
            _ => (0, 0)
        };
    }

    public static (int, int) Invert(int x, int y)
    {
        return (-x, -y);
    }

    public static Direction Invert(Direction direction)
    {
        int x, y;
        (x, y) = ToInts(direction);
        (x, y) = Invert(x, y);
        return ToDirection(x, y);
    }
}
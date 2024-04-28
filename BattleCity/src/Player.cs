using System.Drawing;

namespace BattleCity;

public class Player : Tank
{
    public new event EventHandler OnCreated;
    public new event EventHandler OnMoved;
    public new event EventHandler OnDied;
    public override ConsoleColor GetSpriteColor() => ConsoleColor.Green;

    public Player(Field field, int x, int y)
    {
        X = x;
        Y = y;
        Field = field;
        Field.SubscribeToPlayer(this);
        OnCreated?.Invoke(this, EventArgs.Empty);
    }

    public override void Move(int xDifference, int yDifference)
    {
        if (!CheckMovePosition(xDifference, yDifference)) return;
        X += xDifference;
        Y += yDifference;
        Direction = xDifference switch
        {
            0 when yDifference == -1 => Direction.Up,
            0 when yDifference == 1 => Direction.Down,
            -1 when yDifference == 0 => Direction.Left,
            1 when yDifference == 0 => Direction.Right,
            _ => Direction
        };
        OnMoved?.Invoke(this, EventArgs.Empty);
    }

    public override void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
    }

    public override void ProcessTurn()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    Move(1, 0);
                    break;
                case ConsoleKey.S:
                    Shoot();
                    break;
                case ConsoleKey.Escape:
                    Field.Status = "Escaped";
                    break;
            }
        }
    }
}
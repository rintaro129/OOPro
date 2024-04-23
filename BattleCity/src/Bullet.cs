
namespace Game;

public class Bullet : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnMoved;
    public event EventHandler OnDied;

    public Tank Tank { get; set; }

    public override bool CanMove() => true;
    public override bool IsSolid() => true;

    public override char GetSprite() => '·';
    public override bool IsUnkillable() => false;
    public override int SpeedTicks { get; set; } = 1;

    public Bullet(Tank tank)
    {
        Tank = tank;
        Field = tank.Field;
        Direction = Tank.Direction;
        (int x, int y) = setCoordinates(tank);
        X = x;
        Y = y;
        Field.SubscribeToBullet(this);
        OnCreated?.Invoke(this, EventArgs.Empty);
    }

    public override void Move(int xDifference, int yDifference)
    {
        int x = X +xDifference;
        int y = Y + yDifference;
        if (CheckPositionExploding(x, y))
        {
            TakeDamage();
            if(!CheckPositionOutOfRange(x, y)) Field.Map[x, y].TakeDamage();
            return;
        }
        X = x;
        Y = y;
        OnMoved?.Invoke(this, EventArgs.Empty);
    }

    public override void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
    }

    private (int, int) setCoordinates(Tank tank)
    {
        int x = tank.X;
        int y = tank.Y;
        switch (tank.Direction)
        {
            case Direction.Down:
                y = tank.Y + 1;
                break;
            case Direction.Up:
                y = tank.Y - 1;
                break;
            case Direction.Right:
                x = tank.X + 1;
                break;
            case Direction.Left:
                x = tank.X - 1;
                break;
        }

        return (x, y);
    }

    public override void ProcessTurn()
    {
        switch (Direction)
        {
            case Direction.Down:
                Move(0, 1);
                break;
            case Direction.Up:
                Move(0, -1);
                break;
            case Direction.Right:
                Move(1, 0);
                break;
            case Direction.Left:
                Move(-1, 0);
                break;
        }
    }

    private bool CheckPositionExploding(int x, int y) => CheckPositionOutOfRange(x, y) || CheckPositionIsSolid(x, y);

}
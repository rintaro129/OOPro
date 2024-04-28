namespace BattleCity;

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
        int xDifference, yDifference;
        (xDifference, yDifference) = DirectionUtils.ToInts(Tank.Direction);
        X = Tank.X + xDifference;
        Y = Tank.Y + yDifference;
        Field.SubscribeToBullet(this);
        OnCreated?.Invoke(this, EventArgs.Empty);
    }

    public override void Move(int xDifference, int yDifference)
    {
        int x = X + xDifference;
        int y = Y + yDifference;
        if (CheckPositionExploding(x, y))
        {
            TakeDamage();
            if (!CheckPositionOutOfRange(x, y)) Field.Map[x, y].TakeDamage();
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


    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0) return;
        Move(Direction);
    }

    private bool CheckPositionExploding(int x, int y) => CheckPositionOutOfRange(x, y) || CheckPositionIsSolid(x, y);
}
namespace BattleCity;

public abstract class Tank(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override bool CanMove() => true;
    public override bool IsSolid() => true;
    public override bool IsUnkillable() => false;
    public override int SpeedTicks { get; set; } = 2;
    public override char GetSprite()
    {
        return Direction switch
        {
            Direction.Up => 'Ʌ',
            Direction.Down => 'V',
            Direction.Left => '<',
            Direction.Right => '>',
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override void Move(int xDifference, int yDifference)
    {
        if (!CheckMovePosition(xDifference, yDifference)) return;
        X += xDifference;
        Y += yDifference;
        Direction = DirectionUtils.ToDirection(xDifference, yDifference);
       OnMoved(EventArgs.Empty);
    }

    public Bullet? Bullet { get; set; }
    public bool IsShooting { get; set; }

    public void Shoot()
    {
        if (IsShooting) return;
        int xDifference, yDifference;
        (xDifference, yDifference) = DirectionUtils.ToInts(Direction);
        int x = X + xDifference, y = Y + yDifference;
        if (CheckPositionExploding(x, y))
        {
            if (!CheckPositionOutOfRange(x, y)) Field.Map[x, y].TakeDamage();
            return;
        }
        
        Bullet = new Bullet(Field, X + xDifference, Y + yDifference, this);
        Bullet.Died += HandleBulletDied;
        IsShooting = true;
    }

    private void HandleBulletDied(object? sender, EventArgs e)
    {
        IsShooting = false;
        Bullet = null;
    }

    private bool CheckPositionExploding(int x, int y) => CheckPositionOutOfRange(x, y) || CheckPositionIsSolid(x, y);
}
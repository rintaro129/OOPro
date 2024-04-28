namespace BattleCity;

public class Tank : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnMoved;
    public event EventHandler OnDied;
    public override bool CanMove() => true;
    public override bool IsSolid() => true;
    public override bool IsUnkillable() => false;

    public Tank()
    {
        // for player
    }

    public Tank(Field field, int x, int y)
    {
        X = x;
        Y = y;
        Field = field;
        Field.SubscribeToTank(this);
        OnCreated?.Invoke(this, EventArgs.Empty);
    }


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
        OnMoved?.Invoke(this, EventArgs.Empty);
    }


    public override void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
    }

    public Bullet Bullet { get; set; }
    public bool IsShooting { get; set; } = false;

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

        Bullet = new Bullet(this);
        Bullet.OnDied += Bullet_OnDied;
        IsShooting = true;
    }

    private void Bullet_OnDied(object sender, EventArgs e)
    {
        IsShooting = false;
        Bullet = null;
    }

    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0) return;
        Random random = new Random();
        Direction moveDirection = (Direction)random.Next(5);
        Move(moveDirection);
        Shoot();
    }

    private bool CheckPositionExploding(int x, int y) => CheckPositionOutOfRange(x, y) || CheckPositionIsSolid(x, y);
}
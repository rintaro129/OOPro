namespace Game;

public class Tank : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnMoved;
    public event EventHandler OnDied;
    public override bool CanMove() => true;
    public override bool IsSolid() => true;
    public override bool IsUnkillable() => false;
    public Tank(Field field, int x, int y)
    {
        X = x;
        Y = y;
        Field = field;
        field.SubscribeToTank(this);
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
            Direction.Right => '>'
        };
    }

    public override void Move(int xDifference, int yDifference)
    {
        if (!CheckMovePosition(xDifference, yDifference)) return;
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
}
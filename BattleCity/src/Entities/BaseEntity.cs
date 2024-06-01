namespace BattleCity;

public abstract class BaseEntity
{
    protected BaseEntity(Field field, int x, int y)
    {
        X = x;
        Y = y;
        Field = field;
        Field.SubscribeToEntity(this);
        OnCreated(EventArgs.Empty);
    }

    public event EventHandler? Created;
    public event EventHandler? Moved;
    public event EventHandler? Updated;
    public event EventHandler? Died;

    protected virtual void OnCreated(EventArgs e)
    {
        Created?.Invoke(this, e);
    }

    protected virtual void OnMoved(EventArgs e)
    {
        Moved?.Invoke(this, e);
    }

    protected virtual void OnUpdated(EventArgs e)
    {
        Updated?.Invoke(this, e);
    }

    protected virtual void OnDied(EventArgs e)
    {
        Died?.Invoke(this, e);
    }


    public Field Field { get; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; } = Direction.Up;
    public virtual int HealthPointsMax { get; set; } = 1;
    public virtual int HealthPointsCurrent { get; set; } = 1;
    public virtual int SpeedTicks { get; set; } = 0;
    public abstract bool CanProcessTurn();
    public abstract void Move(int xDifference, int yDifference);

    public void Move(Direction direction)
    {
        int xDifference, yDifference;
        (xDifference, yDifference) = DirectionUtils.ToInts(direction);
        Move(xDifference, yDifference);
    }

    public abstract void ProcessTurn();
    public abstract bool IsSolid();
    public bool IsExplosive() => true;
    public abstract bool IsUnkillable();

    public virtual void TakeDamage(int damageTaken = 1)
    {
        if (!IsUnkillable())
        {
            HealthAdd(-damageTaken);
            if (HealthPointsCurrent == 0)
            {
                OnDied(EventArgs.Empty);
            }
        }
    }

    public virtual void HealthAdd(int healthToAdd)
    {
        HealthPointsCurrent += healthToAdd;
    }

    protected bool CheckMovePosition(int xDifference, int yDifference) =>
        !CheckPositionOutOfRange(X + xDifference, Y + yDifference) &&
        !CheckPositionIsSolid(X + xDifference, Y + yDifference);

    protected bool CheckPositionOutOfRange(int x, int y) =>
        !(x >= 0 && x < Field.FieldSizeX &&
          y >= 0 && y < Field.FieldSizeY);

    private bool CheckPositionIsSolid(int x, int y)
    {
        return Field.Map[x, y] != null &&
               Field.Map[x, y].IsSolid();
    }

    private bool CheckPositionIsExplosive(int x, int y)
    {
        return Field.Map[x, y] != null &&
               Field.Map[x, y].IsExplosive();
    }

    protected bool CheckPositionExploding(int x, int y) =>
        CheckPositionOutOfRange(x, y) || CheckPositionIsExplosive(x, y);
}
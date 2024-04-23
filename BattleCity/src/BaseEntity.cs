using System.Drawing;

namespace Game;

public abstract class BaseEntity
{
    public Field Field { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; } = Direction.Up;
    public virtual int HealthPointsMax { get; set; } = 1;
    public virtual int HealthPointsCurrent { get; set; } = 1;
    public virtual int SpeedTicks { get; set; } = 0;
    public abstract char GetSprite();
    public virtual ConsoleColor GetSpriteColor() => ConsoleColor.White;
    public abstract bool CanMove();
    public abstract void Move(int xDifference, int yDifference);
    public abstract void ProcessTurn();
    public abstract bool IsSolid();
    public abstract bool IsUnkillable();
    public abstract void Die();
    
    public void TakeDamage(int damageTaken = 1)
    {
        if (!IsUnkillable())
        {
            HealthPointsCurrent -= damageTaken;
            if (HealthPointsCurrent <= 0)
            {
                Die();
            }
        }
    }

    protected bool CheckMovePosition(int xDifference, int yDifference) =>
        !CheckPositionOutOfRange(X + xDifference, Y + yDifference) &&
        !CheckPositionIsSolid(X + xDifference, Y + yDifference);

    protected bool CheckPositionOutOfRange(int x, int y) =>
        !(x >= 0 && x < Field.FieldSizeX &&
        y >= 0 && y < Field.FieldSizeY);

    protected bool CheckPositionIsSolid(int x, int y) =>
        Field.Map[x, y] != null &&
        Field.Map[x, y].IsSolid();
}
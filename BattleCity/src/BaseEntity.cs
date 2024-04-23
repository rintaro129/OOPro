using System.Drawing;

namespace Game;

public abstract class BaseEntity
{
    public Field Field { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; } = Direction.Up;
    public virtual int HealthPointsMax { get; } = 1;
    public virtual int HealthPointsCurrent { get; set; } = 1;
    public virtual int SpeedTicks { get; set; } = 0;
    public abstract char GetSprite();
    public virtual Color GetSpriteColor() => Color.White;
    public abstract bool CanMove();
    public abstract void Move(int xDifference, int yDifference);
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

    protected bool CheckMovePosition(int xDifference, int yDifference)
    {
        return X + xDifference >= 0 && X + xDifference < Field.FieldSizeX &&
                Y + yDifference >= 0 && Y + yDifference < Field.FieldSizeY &&
                (Field.Map[X + xDifference, Y + yDifference] == null ||
                 !Field.Map[X + xDifference, Y + yDifference].IsSolid());
    }
}
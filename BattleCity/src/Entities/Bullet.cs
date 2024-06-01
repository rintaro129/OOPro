namespace BattleCity;

public class Bullet : BaseEntity
{
    public Tank Tank { get; set; }

    public override bool CanProcessTurn() => true;
    public override bool IsSolid() => true;

    public override bool IsUnkillable() => false;
    public override int SpeedTicks { get; set; } = 4;

    public Bullet(Field field, int x, int y, Tank tank) : base(field, x, y)
    {
        Tank = tank;
        Direction = Tank.Direction;
    }

    public override void Move(int xDifference, int yDifference)
    {
        int x = X + xDifference;
        int y = Y + yDifference;
        if (CheckPositionExploding(x, y))
        {
            TakeDamage();
            if (!CheckPositionOutOfRange(x, y))
            {
                if (Field.Map[x, y] is Tank { HealthPointsCurrent: 1 } && Tank is Player player)
                {
                    player.ScoreAdd(1);
                }

                Field.Map[x, y].TakeDamage();
            }

            return;
        }

        X = x;
        Y = y;
        OnMoved(EventArgs.Empty);
    }

    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0) 
            return;
        Move(Direction);
    }
}
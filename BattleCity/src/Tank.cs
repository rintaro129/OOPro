namespace BattleCity;

public abstract class Tank(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override bool CanProcessTurn() => true;
    public override bool IsSolid() => true;
    public override bool IsUnkillable() => false;
    public bool WasDamaged { get; set; }
    public override int SpeedTicks { get; set; } = 8;

    public override void Move(int xDifference, int yDifference)
    {
        if (xDifference == 0 && yDifference == 0) return;
        if (DirectionUtils.ToDirection(xDifference, yDifference) != Direction)
        {
            Direction = DirectionUtils.ToDirection(xDifference, yDifference);
            OnUpdated(EventArgs.Empty);
            return;
        }

        if (!CheckMovePosition(xDifference, yDifference)) return;
        if (Field.Map[X + xDifference, Y + yDifference] is Prize prize)
        {
            prize.GrantPrize(this);
        }

        X += xDifference;
        Y += yDifference;
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
            if (!CheckPositionOutOfRange(x, y))
            {
                if (Field.Map[x, y] is Tank { HealthPointsCurrent: 1 } && this is Player player)
                {
                    player.ScoreAdd(1);
                }

                Field.Map[x, y].TakeDamage();
            }

            return;
        }

        Bullet = new Bullet(Field, X + xDifference, Y + yDifference, this);
        Bullet.Died += HandleBulletDied;
        IsShooting = true;
    }

    public override void TakeDamage(int damageTaken = 1)
    {
        WasDamaged = true;
        OnUpdated(EventArgs.Empty);
        base.TakeDamage(damageTaken);
    }

    private void HandleBulletDied(object? sender, EventArgs e)
    {
        IsShooting = false;
        Bullet = null;
    }
}
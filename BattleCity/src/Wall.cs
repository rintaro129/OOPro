namespace BattleCity;

public class Wall : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnDied;
    public override bool CanMove() => false;
    public override bool IsSolid() => true;
    public override char GetSprite() => '#';
    public override bool IsUnkillable() => false;
    public override int HealthPointsMax { get; set; } = 2;
    public override int HealthPointsCurrent { get; set; } = 2;

    public Wall(Field field, int x, int y)
    {
        Field = field;
        X = x;
        Y = y;
        Field.SubscribeToWall(this);
        OnCreated?.Invoke(this, EventArgs.Empty);
    }

    public override void Move(int xDifference, int yDifference)
    {
        Console.WriteLine("Don't even try... You can not...");
    }

    public override void ProcessTurn()
    {
        Console.WriteLine("Don't even try... You can not...");
    }

    public override void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
    }
}
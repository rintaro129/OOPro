namespace BattleCity;

public abstract class Obstacle(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override bool CanProcessTurn() => false;
    public override bool IsSolid() => true;
    public override bool IsUnkillable() => false;

    public override void Move(int xDifference, int yDifference)
    {
    }

    public override void ProcessTurn()
    {
    }
}
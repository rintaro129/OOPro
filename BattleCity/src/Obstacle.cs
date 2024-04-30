namespace BattleCity;

public class Obstacle(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override bool CanMove() => false;
    public override bool IsSolid() => true;
    public override char GetSprite() => '#';
    public override bool IsUnkillable() => false;
    public override int HealthPointsMax { get; set; } = 2;
    public override int HealthPointsCurrent { get; set; } = 2;

    public override void Move(int xDifference, int yDifference)
    {
    }

    public override void ProcessTurn()
    {
    }
    
}
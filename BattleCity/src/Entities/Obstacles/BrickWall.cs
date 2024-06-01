namespace BattleCity;

public class BrickWall(Field field, int x, int y) : Obstacle(field, x, y)
{
    public override string GetName() => "Brick Wall";
    public override int HealthPointsMax { get; set; } = 2;
    public override int HealthPointsCurrent { get; set; } = 2;
}
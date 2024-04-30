namespace BattleCity;

public class SteelWall(Field field, int x, int y) : Obstacle(field, x, y)
{
    public override bool IsUnkillable() => true;
}
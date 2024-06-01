namespace BattleCity;

public class EnemyLvl2(Field field, int x, int y) : EnemyLvl1(field, x, y)
{
    public override int HealthPointsCurrent { get; set; } = 2;
    public override int HealthPointsMax { get; set; } = 2;
}
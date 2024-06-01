namespace BattleCity;

public class EnemyLvl3(Field field, int x, int y) : EnemyLvl2(field, x, y)
{
    public override string GetName() => "Enemy Tank (Level 3)";
    public override int SpeedTicks { get; set; } = 4;
}
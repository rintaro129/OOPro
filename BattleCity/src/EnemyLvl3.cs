namespace BattleCity;

public class EnemyLvl3(Field field, int x, int y) : EnemyLvl2(field, x, y)
{
    public override int SpeedTicks { get; set; } = 4;
}
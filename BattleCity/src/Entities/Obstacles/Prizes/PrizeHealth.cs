namespace BattleCity;

public class PrizeHealth(Field field, int x, int y) : Prize(field, x, y)
{
    public override string GetName() => "Prize (health buff)";
    public override void GrantPrize(Tank tank)
    {
        tank.HealthAdd(1);
    }
}
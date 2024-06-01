namespace BattleCity;

public class PrizeSpeed(Field field, int x, int y) : Prize(field, x, y)
{
    public override void GrantPrize(Tank tank)
    {
        if (tank.SpeedTicks >= 2)
            tank.SpeedTicks /= 2;
    }
}
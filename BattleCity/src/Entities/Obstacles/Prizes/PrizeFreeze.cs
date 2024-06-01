namespace BattleCity;

public class PrizeFreeze(Field field, int x, int y) : Prize(field, x, y)
{
    public override string GetName() => "Prize (freeze)";
    public override void GrantPrize(Tank tank)
    {
        Field.FreezeLeftForTicks = 200;
        Field.FreezeExceptionTank = tank;
    }
}
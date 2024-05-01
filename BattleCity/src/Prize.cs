namespace BattleCity;
public abstract class Prize(Field field, int x, int y) : Obstacle(field, x, y)
{
    public override bool IsSolid() => false;
    public abstract void GrantPrize(Tank tank);
}


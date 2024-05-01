namespace BattleCity;

public class Explosion(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override bool IsUnkillable() => true;
    public override bool CanProcessTurn() => true;
    public override bool IsSolid() => true;

    public override int SpeedTicks { get; set; } = 4;
    private int TicksPassed { get; set; }
    const int TicksPassedMax = 5;
    public override void Move(int xDifference, int yDifference)
    {
        
    }

    public override void ProcessTurn()
    {
        if(TicksPassedMax == TicksPassed)
            OnDied(EventArgs.Empty);
        TicksPassed++;
    }
}
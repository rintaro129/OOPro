namespace BattleCity;

public class Spawn(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override bool IsUnkillable() => true;
    public override bool CanProcessTurn() => true;
    public override bool IsSolid() => true;
    public override int SpeedTicks { get; set; } = 2;
    public int TicksPassed { get; set; } = 0;
    const int TicksPassedMax = 40;
    public override void Move(int xDifference, int yDifference)
    {
    }

    protected override void OnDied(EventArgs e)
    {
        base.OnDied(e);
        Random random = new Random();
        int randomNumber = random.Next(3);
        switch (randomNumber)
        {
            case 0:
                Field.Map[X, Y] = new EnemyLvl1(Field, X, Y);
                break;
            case 1:
                Field.Map[X, Y] = new EnemyLvl2(Field, X, Y);
                break;
            case 2:
                Field.Map[X, Y] = new EnemyLvl3(Field, X, Y);
                break;
        }
    }

    public override void ProcessTurn()
    {
        if (TicksPassedMax == TicksPassed)
        {
            OnDied(EventArgs.Empty);
            return;
        }

        TicksPassed++;
        OnUpdated(EventArgs.Empty);
    }
}
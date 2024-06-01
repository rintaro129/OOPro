namespace BattleCity;

public class Spawn(Field field, int x, int y) : BaseEntity(field, x, y)
{
    public override string GetName() => "Spawn";
    public event EventHandler? SpawnTriggered;
    public override bool IsUnkillable() => true;
    public override bool CanProcessTurn() => true;
    public override bool IsSolid() => true;
    public override int SpeedTicks { get; set; } = 4;
    public int TurnsPassed { get; set; } = 0;
    const int TurnsPassedMax = 40;

    public override void Move(int xDifference, int yDifference)
    {
    }

    protected override void OnDied(EventArgs e)
    {
        base.OnDied(e);
        Random random = new Random();
        int randomNumber = random.Next(6);
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
            case 3:
                Field.Map[X, Y] = new PrizeHealth(Field, X, Y);
                break;
            case 4:
                Field.Map[X, Y] = new PrizeSpeed(Field, X, Y);
                break;
            case 5:
                Field.Map[X, Y] = new PrizeFreeze(Field, X, Y);
                break;
        }

        SpawnTriggered?.Invoke(this, new VisualEntityEventArgs(Field.Map[X, Y]));
    }

    public override void ProcessTurn()
    {
        if (TurnsPassedMax == TurnsPassed)
        {
            OnDied(EventArgs.Empty);
            return;
        }

        TurnsPassed++;
        OnUpdated(EventArgs.Empty);
    }
}
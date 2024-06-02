namespace BattleCity;

public class Player : Tank 
{
    public override string GetName() => "Player Tank";
    public Player(Field field, int x, int y, int score) : base(field, x, y)
    {
        Score = score;
        StatsUpdated?.Invoke(this, EventArgs.Empty);
    }
    public event EventHandler? StatsUpdated;
    public override int HealthPointsCurrent { get; set; } = 2;
    public override int HealthPointsMax { get; set; } = 2;
    public int Score { get; set; }

    public void ScoreAdd(int value)
    {
        Score += value;
        StatsUpdated?.Invoke(this, EventArgs.Empty);
    }

    public override void HealthAdd(int healthToAdd)
    {
        base.HealthAdd(healthToAdd);
        StatsUpdated?.Invoke(this, EventArgs.Empty);
    }

    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0)
            return;
        WasDamaged = false;
        OnUpdated(EventArgs.Empty);
        string key = Field.IO.AskForInput();

        switch (key)
        {
            case "up":
                Move(0, -1);
                break;
            case "down":
                Move(0, 1);
                break;
            case "left":
                Move(-1, 0);
                break;
            case "right":
                Move(1, 0);
                break;
            case "shoot":
                Shoot();
                break;
            case "escape":
                Field.Status = "Escaped";
                break;
        }
    }
}
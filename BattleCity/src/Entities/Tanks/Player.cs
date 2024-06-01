namespace BattleCity;

public class Player : Tank 
{
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
        if (HealthPointsCurrent <= 0) return;
        WasDamaged = false;
        OnUpdated(EventArgs.Empty);
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    Move(1, 0);
                    break;
                case ConsoleKey.S:
                    Shoot();
                    break;
                case ConsoleKey.Escape:
                    Field.Status = "Escaped";
                    break;
            }
        }
    }
}
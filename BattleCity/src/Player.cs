namespace BattleCity;

public class Player(Field field, int x, int y) : Tank(field, x, y)
{
    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0) return;
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
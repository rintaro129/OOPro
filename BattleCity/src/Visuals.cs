namespace BattleCity;

public class Visuals
{
    public Visuals(Field field)
    {
        field.EntityCreated += HandleEntityCreated;
        field.EntityDeleted += HandleEntityDeleted;
        field.LevelStarting += HandleLevelStarting;
    }

    private void HandleLevelStarting(object? sender, EventArgs e)
    {
        Console.Clear();
    }

    private void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (e is VisualEntityEventArgs args)
        {
            Console.ForegroundColor = args.Color;
            Console.BackgroundColor = args.BackgroundColor;
            Console.SetCursorPosition(args.X, args.Y);
            Console.Write(args.Sprite);
            Console.ResetColor();
        }
    }

    private void HandleEntityDeleted(object? sender, EventArgs e)
    {
        if (e is VisualEntityEventArgs args)
        {
            Console.SetCursorPosition(args.X, args.Y);
            Console.Write(' ');
        }
    }
}
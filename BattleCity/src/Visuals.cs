namespace BattleCity;

public static class Visuals
{
    public static void ConnectVisuals(Field field)
    {
        field.EntityCreated += HandleEntityCreated;
        field.EntityDeleted += HandleEntityDeleted;
        field.LevelStarting += HandleLevelStarting;
    }

    private static void HandleLevelStarting(object? sender, EventArgs e)
    {
        Console.Clear();
    }

    private static void HandleEntityCreated(object? sender, EventArgs e)
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

    private static void HandleEntityDeleted(object? sender, EventArgs e)
    {
        if (e is VisualEntityEventArgs args)
        {
            Console.SetCursorPosition(args.X, args.Y);
            Console.Write(' ');
        }
    }
}
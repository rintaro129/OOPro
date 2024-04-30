namespace BattleCity;

public class Visuals
{
    public Visuals(Field field)
    {
        field.OnEntityCreated += Field_OnEntityCreated;
        field.OnEntityDeleted += Field_OnEntityDeleted;
        field.LevelStarting += HandleLevelStarting;
    }

    private void HandleLevelStarting(object? sender, EventArgs e)
    {
        Console.Clear();
    }
    private void Field_OnEntityCreated(object? sender, EventArgs e)
    {
        if (e is VisualEntityEventArgs args)
        {
            Console.ForegroundColor = args.Color;
            Console.SetCursorPosition(args.X, args.Y);
            Console.Write(args.Sprite);
            Console.ResetColor();
        }
    }

    private void Field_OnEntityDeleted(object? sender, EventArgs e)
    {
        if (e is VisualEntityEventArgs args)
        {
            Console.SetCursorPosition(args.X, args.Y);
            Console.Write(' ');
        }
    }
}
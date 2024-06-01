namespace BattleCity;

public static class ConsoleIO
{
    public static void ConnectVisuals(Field field)
    {
        field.EntityCreated += HandleEntityCreated;
        field.EntityDeleted += HandleEntityDeleted;
        field.LevelStarting += HandleLevelStarting;
        field.LevelStarted += HandleLevelStarted;
    }

    public static void SubscribeToPlayer(Player player)
    {
        player.StatsUpdated += HandlePlayerStatsUpdated;
    }

    public static void SubscribeToSpawn(Spawn spawn)
    {
        spawn.SpawnTriggered += HandleSpawnTriggered;
        spawn.Created += HandleSpawnsCountUpdate;
    }


    private static void HandleLevelStarted(object? sender, EventArgs e)
    {
        if (sender is not Field field) return;
        Console.SetCursorPosition(0, field.FieldSizeY);
        Console.WriteLine(field.Name);
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

    private static void HandlePlayerStatsUpdated(object? sender, EventArgs e)
    {
        if (sender is not Player player) return;
        Field field = player.Field;
        Console.ResetColor();
        Console.SetCursorPosition(0, field.FieldSizeY + 3);
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, field.FieldSizeY + 4);
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, field.FieldSizeY + 3);
        Console.Write("Player Health Points ");
        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 0; i < player.HealthPointsCurrent; i++)
        {
            Console.Write('\u2665');
        }

        Console.ResetColor();
        Console.SetCursorPosition(0, field.FieldSizeY + 4);
        Console.WriteLine($"Score: {player.Score}");
    }

    private static void HandleSpawnTriggered(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn || e is not VisualEntityEventArgs ve) return;
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + 1);
        Console.ResetColor();
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + 1);
        Console.WriteLine($"{ve.Entity.GetType().Name} has appeared!");
    }

    private static void HandleSpawnsCountUpdate(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn) return;
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + 2);
        Console.ResetColor();
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + 2);
        Console.Write("Spawns Left: ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        for (int i = 0; i < spawn.Field.EntitiesToSpawnCount - 1; i++) // EntitiesToSpawnCount will decrease only after spawn triggering
        {
            Console.Write('+');
        }

        Console.ResetColor();
    }
}
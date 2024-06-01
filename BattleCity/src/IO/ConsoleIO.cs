using System;

namespace BattleCity;

public static class ConsoleIO
{
    private const int SPAWN_INFO_LINE = 1;
    private const int SPAWNS_LEFT_INFO_LINE = 2;
    private const int HEALTH_INFO_LINE = 3;
    private const int SCORE_INFO_LINE = 4;
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
        spawn.SpawnTriggered += HandleSpawnsCountUpdate;
    }


    private static void HandleLevelStarted(object? sender, EventArgs e)
    {
        if (sender is not Field field) 
            return;
        Console.SetCursorPosition(0, field.FieldSizeY);
        Console.WriteLine(field.Name);
        Console.SetCursorPosition(0, field.FieldSizeY + SPAWNS_LEFT_INFO_LINE);
        Console.Write("Spawns Left: ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        for (int i = 0; i < field.EntitiesToSpawnCount; i++)
        {
            Console.Write('+');
        }
        Console.ResetColor();
    }

    private static void HandleLevelStarting(object? sender, EventArgs e)
    {
        Console.Clear();
    }

    private static void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (e is not VisualEntityEventArgs args)
        {
            return;
        }
        Console.ForegroundColor = args.Color;
        Console.BackgroundColor = args.BackgroundColor;
        Console.SetCursorPosition(args.X, args.Y);
        Console.Write(args.Sprite);
        Console.ResetColor();
    }

    private static void HandleEntityDeleted(object? sender, EventArgs e)
    {
        if (e is not VisualEntityEventArgs args)
        {
            return;
        }
        Console.SetCursorPosition(args.X, args.Y);
        Console.Write(' ');
    }

    private static void HandlePlayerStatsUpdated(object? sender, EventArgs e)
    {
        if (sender is not Player player) 
            return;
        Field field = player.Field;
        Console.ResetColor();
        Console.SetCursorPosition(0, field.FieldSizeY + HEALTH_INFO_LINE);
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, field.FieldSizeY + SCORE_INFO_LINE);
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, field.FieldSizeY + HEALTH_INFO_LINE);
        Console.Write("Player Health Points ");
        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 0; i < player.HealthPointsCurrent; i++)
        {
            Console.Write('\u2665');
        }

        Console.ResetColor();
        Console.SetCursorPosition(0, field.FieldSizeY + SCORE_INFO_LINE);
        Console.WriteLine($"Score: {player.Score}");
    }

    private static void HandleSpawnTriggered(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn || e is not VisualEntityEventArgs ve) 
            return;
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + SPAWN_INFO_LINE);
        Console.ResetColor();
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + SPAWN_INFO_LINE);
        Console.WriteLine($"{ve.Entity.GetName()} has appeared!");
    }

    private static void HandleSpawnsCountUpdate(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn) 
            return;
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + SPAWNS_LEFT_INFO_LINE);
        Console.ResetColor();
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + SPAWNS_LEFT_INFO_LINE);
        Console.Write("Spawns Left: ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        for (int i = 0; i < spawn.Field.EntitiesToSpawnCount; i++) 
        {
            Console.Write('+');
        }

        Console.ResetColor();
    }
}
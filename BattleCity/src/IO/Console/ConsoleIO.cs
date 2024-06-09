using System;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace BattleCity;

public class ConsoleIO : BaseIO
{
    private const int SPAWN_INFO_LINE = 1;
    private const int SPAWNS_LEFT_INFO_LINE = 2;
    private const int HEALTH_INFO_LINE = 3;
    private const int SCORE_INFO_LINE = 4;
    public override void ConnectIO(Field field)
    {
        field.EntityCreated += HandleEntityCreated;
        field.EntityDeleted += HandleEntityDeleted;
        field.LevelStarting += HandleLevelStarting;
        field.LevelStarted += HandleLevelStarted;
    }

    public override void SubscribeToPlayer(Player player)
    {
        player.StatsUpdated += HandlePlayerStatsUpdated;
    }

    public override void SubscribeToSpawn(Spawn spawn)
    {
        spawn.SpawnTriggered += HandleSpawnTriggered;
        spawn.SpawnTriggered += HandleSpawnsCountUpdate;
    }

    private void HandleLevelStarted(object? sender, EventArgs e)
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

    private void HandleLevelStarting(object? sender, EventArgs e)
    {
        Console.Clear();
    }

    private void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (e is not IOEventArgs args)
        {
            return;
        }
        Console.ForegroundColor = args.Color;
        Console.BackgroundColor = args.BackgroundColor;
        Console.SetCursorPosition(args.X, args.Y);
        Console.Write(args.Sprite);
        Console.ResetColor();
    }

    private void HandleEntityDeleted(object? sender, EventArgs e)
    {
        if (e is not IOEventArgs args)
        {
            return;
        }
        Console.SetCursorPosition(args.X, args.Y);
        Console.Write(' ');
    }

    private void HandlePlayerStatsUpdated(object? sender, EventArgs e)
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

    private void HandleSpawnTriggered(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn || e is not IOEventArgs ve)
            return;
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + SPAWN_INFO_LINE);
        Console.ResetColor();
        Console.WriteLine("                                                       ");
        Console.SetCursorPosition(0, spawn.Field.FieldSizeY + SPAWN_INFO_LINE);
        Console.WriteLine($"{ve.Entity.GetName()} has appeared!");
    }

    private void HandleSpawnsCountUpdate(object? sender, EventArgs e)
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

    public override void AskName()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.Unicode;
        Console.Clear();
        Console.WriteLine("What's your name?");
        Game.Name = Console.ReadLine();
    }
    public override void ShowStartMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(@$"Welcome in Battle City, {Game.Name}
Destroy all the enemy tanks!
Collect prizes!
Avoid bullets!
Don't explode by bombs!
And pass all the levels!
You can create levels by yourself, read README.md for this!
 
 - To start the campaign type 'campaign' (without quotation marks)
 - To view the Scoreboard type 'scoreboard'
 - To start random mode type 'random'
 - To quit type 'quit'");
            string option = Console.ReadLine();
            switch (option)
            {
                case "campaign":
                    Game.StartCampaign();
                    break;
                case "scoreboard":
                    Game.ViewScoreboard();
                    break;
                case "random":
                    Game.StartRandomMode();
                    break;
            }

            if (option == "quit")
                break;
        }
    }
    public override void ShowScoreboard(List<GameResult> gameResults)
    {
        Console.Clear();
        Console.WriteLine("Scoreboard");
        Console.WriteLine("{0, -15} {1, -20} {2, -10} {3, -20}\n", "Name", "Level", "Score", "Time Elapsed");
        foreach (GameResult gameResult in gameResults)
        {
            Console.WriteLine("{0, -15} {1, -20} {2, -10} {3, -20:mm\\:ss}",
                gameResult.Name, gameResult.Level, gameResult.Score, gameResult.TimeElapsed);
        }

        Console.WriteLine("\n Press any key to continue...");
        Console.ReadKey();
    }
    public override string AskForInput()
    {
        if (!Console.KeyAvailable)
        {
            return "";
        }
        ConsoleKeyInfo key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                return "up";
            case ConsoleKey.DownArrow:
                return "down";
            case ConsoleKey.LeftArrow:
                return "left";
            case ConsoleKey.RightArrow:
                return "right";
            case ConsoleKey.S:
                return "shoot";
            case ConsoleKey.Escape:
                return "escape";
        }
        return "";
    }

    public override void ShowLevelFinishedMessage(Field field)
    {
        if (field.Name == "Random Mode")
        {
            Console.WriteLine(field.Status);
            return;
        }
        Console.SetCursorPosition(0, field.FieldSizeY + 5);
        Console.WriteLine(field.Status);
        switch (field.Status)
        {
            case "Escaped":
            case "Player Died :(":
                Console.WriteLine("Your score has been saved.");
                PressEnter();
                return;
            case "Enemies are defeated!":
                Console.WriteLine("Onto the next level...");
                PressEnter();
                break;
        }
    }
    public void PressEnter()
    {
        Console.WriteLine("Press Enter to continue...");
        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
        }
    }
    public override void ShowCongratulation()
    {
        Console.WriteLine("Congratulations! You have beaten all the levels!");
        Console.WriteLine("Your score has been saved.");
        PressEnter();
    }

}
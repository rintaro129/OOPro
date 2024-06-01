using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BattleCity;

public class Game
{
    private const int tickSeconds = 25;
    private string name { get; set; }
    public const string ResDirectory = "res";


    public void Start()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.Unicode;
        Console.Clear();
        Console.WriteLine("What's your name?");
        name = Console.ReadLine();
        while (true)
        {
            Console.Clear();
            Console.WriteLine(@$"Welcome in Battle City, {name}
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
                    StartCampaign();
                    break;
                case "scoreboard":
                    ViewScoreboard();
                    break;
                case "random":
                    StartRandomMode();
                    break;
            }

            if (option == "quit") 
                break;
        }
    }

    private string[] GetLevelPaths()
    {
        string[] files = Directory.GetFiles(ResDirectory, "*.lvl");
        return files;
    }

    private void AddGameResult(GameResult gameResult)
    {
        List<GameResult> gameResults = GetListGameResults();
        gameResults.Add(gameResult);
        SetGameResults(gameResults);
    }
    public void PressEnter()
    {
        Console.WriteLine("Press Enter to continue...");
        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
        { 
        }
    }
    private void StartCampaign()
    {
        string[] levelPaths = GetLevelPaths();
        Field field = new Field();
        GameResult gameResult = new GameResult() { Name = name, Level = "0", Score = 0 };
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (string filepath in levelPaths)
        {
            field.Start(filepath, gameResult.Score);
            field.Play(tickSeconds);
            gameResult.Score += field.Player.Score;
            Console.SetCursorPosition(0, field.FieldSizeY + 5);
            Console.WriteLine(field.Status);
            switch (field.Status)
            {
                case "Escaped":
                case "Player Died :(":
                    stopwatch.Stop();
                    gameResult.TimeElapsed = stopwatch.Elapsed;
                    AddGameResult(gameResult);
                    Console.WriteLine("Your score has been saved.");
                    PressEnter();
                    return;
                case "Enemies are defeated!":
                    gameResult.Level = Path.GetFileNameWithoutExtension(filepath);
                    Console.WriteLine("Onto the next level...");
                    PressEnter();
                    break;
            }
        }
        Console.WriteLine("Congratulations! You have beaten all the levels!");
        stopwatch.Stop();
        gameResult.TimeElapsed = stopwatch.Elapsed;
        AddGameResult(gameResult);
        Console.WriteLine("Your score has been saved.");
        PressEnter();
    }

    private void StartRandomMode()
    {
        Field field = new Field();
        field.StartRandom();
        field.Play(tickSeconds);
        Console.SetCursorPosition(0, field.FieldSizeY + 5);
        Console.WriteLine(field.Status);
        PressEnter();
    }

    private void ViewScoreboard()
    {
        Console.Clear();
        Console.WriteLine("Scoreboard");
        List<GameResult> gameResults = GetListGameResults();
        gameResults.Sort();
        Console.WriteLine("{0, -15} {1, -20} {2, -10} {3, -20}\n", "Name", "Level", "Score", "Time Elapsed");
        foreach (GameResult gameResult in gameResults)
        {
            Console.WriteLine("{0, -15} {1, -20} {2, -10} {3, -20:mm\\:ss}", 
                gameResult.Name, gameResult.Level, gameResult.Score, gameResult.TimeElapsed);
        }

        Console.WriteLine("\n Press any key to continue...");
        Console.ReadKey();
    }

    private List<GameResult> GetListGameResults()
    {
        string filePath = Path.Combine(ResDirectory, "Scoreboard.json");
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.Write("[]");
            }
        }

        string jsonString = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<GameResult>>(jsonString);
    }

    private void SetGameResults(List<GameResult> gameResults)
    {
        string filePath = Path.Combine(ResDirectory, "Scoreboard.json");
        string newJsonString = JsonConvert.SerializeObject(gameResults, Formatting.Indented);
        File.WriteAllText(filePath, newJsonString);
    }
}
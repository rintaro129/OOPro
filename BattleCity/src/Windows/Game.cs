using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BattleCity;

public class Game(BaseIO IO)
{
    public BaseIO IO { get; } = IO;
    private const int tickSeconds = 25;
    public string Name { get; set; }
    public const string ResDirectory = "res";


    public void Start()
    {
        IO.AskName();
        StartMenu();
    }
    public void StartMenu()
    {
        IO.ShowStartMenu();
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
    public void StartCampaign()
    {
        string[] levelPaths = GetLevelPaths();
        Field field = new Field(IO);
        GameResult gameResult = new GameResult() { Name = Name, Level = "0", Score = 0 };
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

    public void StartRandomMode()
    {
        Field field = new Field(IO);
        field.StartRandom();
        field.Play(tickSeconds);
        Console.SetCursorPosition(0, field.FieldSizeY + 5);
        Console.WriteLine(field.Status);
        PressEnter();
    }

    public void ViewScoreboard()
    {
        List<GameResult> gameResults = GetListGameResults();
        gameResults.Sort();
        IO.ShowScoreboard(gameResults);
    }

    public List<GameResult> GetListGameResults()
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
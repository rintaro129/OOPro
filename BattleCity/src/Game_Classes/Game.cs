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
            switch (field.Status)
            {
                case "Escaped":
                case "Player Died :(":
                    stopwatch.Stop();
                    gameResult.TimeElapsed = stopwatch.Elapsed;
                    AddGameResult(gameResult);
                    IO.ShowLevelFinishedMessage(field);
                    return;
                case "Enemies are defeated!":
                    gameResult.Level = Path.GetFileNameWithoutExtension(filepath);
                    break;
            }
            IO.ShowLevelFinishedMessage(field);
        }
        stopwatch.Stop();
        gameResult.TimeElapsed = stopwatch.Elapsed;
        AddGameResult(gameResult);
        IO.ShowCongratulation();
    }

    public void StartRandomMode()
    {
        Field field = new Field(IO);
        field.StartRandom();
        field.Play(tickSeconds);
        IO.ShowLevelFinishedMessage(field);
    }

    public void ViewScoreboard()
    {
        List<GameResult> gameResults = GetListGameResults();
        gameResults.Sort();
        IO.ShowScoreboard(gameResults);
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
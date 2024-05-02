using Newtonsoft.Json;
namespace BattleCity;

public class GameResult : IComparable<GameResult>
{
    [JsonProperty("Level")]
    public string Level { get; set; }

    [JsonProperty("TimeElapsed")]
    public TimeSpan TimeElapsed { get; set; }

    [JsonProperty("Score")]
    public int Score { get; set; }
    
    [JsonProperty("Name")]
    public string Name { get; set; }

    public int CompareTo(GameResult? other)
    {
        if (other == null)
            return 1;
        int result = other.Level.CompareTo(this.Level);
        if (result != 0)
            return result;
        result = other.Score.CompareTo(this.Score);
        if (result != 0)
            return result;
        result = this.TimeElapsed.CompareTo(other.TimeElapsed);
        if (result != 0)
            return result;
        return this.Name.CompareTo(other.Name);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity;

public abstract class BaseIO
{
    public Game Game { get; set; }
    public abstract void ConnectIO(Field field);
    public abstract void SubscribeToPlayer(Player player);
    public abstract void SubscribeToSpawn(Spawn spawn);
    public abstract void AskName();
    public abstract void ShowStartMenu();
    public abstract void ShowScoreboard(List<GameResult> gameResults);
    public abstract string AskForInput();
    public abstract void ShowLevelFinishedMessage(Field field);
    public abstract void ShowCongratulation();
}

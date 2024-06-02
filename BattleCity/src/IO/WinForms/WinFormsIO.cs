using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BattleCity;

public class WinFormsIO : BaseIO
{
    public System.Drawing.Size CurrentSize { get; set; } = new System.Drawing.Size(600, 400);
    public System.Drawing.Point CurrentLocation { get; set; } = new System.Drawing.Point(600, 400);
    public WinFormsIO() {
        ApplicationConfiguration.Initialize();
    }
    public override void ConnectIO(Field field)
    {
        throw new NotImplementedException();
    }
    public override void SubscribeToPlayer(Player player)
    {
        throw new NotImplementedException();
    }
    public override void SubscribeToSpawn(Spawn spawn)
    {
        throw new NotImplementedException();
    }
    public override void AskName()
    {
        NameForm nameForm = new NameForm(Game, CurrentSize, CurrentLocation);
        Application.Run(nameForm);
        CurrentLocation = nameForm.Location;
        CurrentSize = nameForm.Size;
    }
    public override void ShowStartMenu()
    {
        StartMenu menu = new StartMenu(Game, CurrentSize, CurrentLocation);
        Application.Run(menu);
        CurrentLocation = menu.Location;
        CurrentSize = menu.Size;
        switch (menu.ExitStatus) {
            case "scoreboard":
                Game.ViewScoreboard();
                break;
            case "random":
                Game.StartRandomMode();
                break;
            case "campaign":
                Game.StartCampaign();
                break;
        }

    }
    public override void ShowScoreboard(List<GameResult> gameResults)
    {
        ScoreboardForm scoreboard = new ScoreboardForm(Game, gameResults, CurrentSize, CurrentLocation);
        Application.Run(scoreboard);
        CurrentLocation = scoreboard.Location;
        CurrentSize = scoreboard.Size;
        switch(scoreboard.ExitStatus)
        {
            case "startmenu":
                Game.StartMenu();
                break;
        }
    }
    public override string AskForInput()
    {
        throw new NotImplementedException();
    }

}

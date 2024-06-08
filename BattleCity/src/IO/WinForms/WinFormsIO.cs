using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace BattleCity;

public class WinFormsIO : BaseIO
{
    public System.Drawing.Size CurrentSize { get; set; } = new System.Drawing.Size(600, 400);
    public System.Drawing.Point CurrentLocation { get; set; } = new System.Drawing.Point(600, 400);
    public LevelForm LevelForm { get; set; }
    public Dictionary<BaseEntity, PictureBox> PictureBoxes { get; set; } = new Dictionary<BaseEntity, PictureBox>();
    public Point FieldStart { get; set; }
    public const int TILE_HEIGHT = 32;
    public const int TILE_WIDTH = 32;
    public const int RIGHT_OFFSET = 350;
    public int FieldSizeX { get; set; }
    public int FieldSizeY { get; set; }

    public WinFormsIO()
    {
        ApplicationConfiguration.Initialize();
    }
    public override void ConnectIO(Field field)
    {
        field.EntityCreated += HandleEntityCreated;
        field.EntityDeleted += HandleEntityDeleted;
        field.EntityMoved += HandleEntityMoved;
        field.LevelStarting += HandleLevelStarting;
        field.LevelStarted += HandleLevelStarted;
        field.SizeSet += HandleSizeSet;

        LevelForm = new LevelForm();
        Thread uiThread = new Thread(() =>
        {
            Application.Run(LevelForm);
        });
        uiThread.SetApartmentState(ApartmentState.STA);
        uiThread.Start();
        Thread.Sleep(1000);
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
        LevelForm.LevelNameTextBox.Text = field.Name;

        string res = "Spawns Left: ";
        for (int i = 0; i < field.EntitiesToSpawnCount; i++)
            res += "+";
        LevelForm.SpawnTextBox.Text = res;
    }
    private void HandleLevelStarting(object? sender, EventArgs e)
    {
        PictureBoxes.Clear();

        if (LevelForm.InvokeRequired)
        {
            LevelForm.Invoke(new Action(() => { LevelForm.Controls.Clear();
                LevelForm.AddTextBoxes();
            }));
        }
        else
        {
            LevelForm.Controls.Clear();
            LevelForm.AddTextBoxes();
        }
     
    }

    private void HandleSizeSet(object? sender, EventArgs e)
    {
        if (sender is not Field field)
        {
            throw new ArgumentException();
        }
        FieldSizeX = field.FieldSizeX;
        FieldSizeY = field.FieldSizeY;
        PutFieldBackground();
    }

    private void PutFieldBackground()
    {
        PictureBox background = new PictureBox();

        Point center = new Point((LevelForm.Size.Width - RIGHT_OFFSET) / 2, LevelForm.Size.Height / 2);
        int width = FieldSizeX * TILE_WIDTH;
        int height = FieldSizeY * TILE_HEIGHT;
        FieldStart = new Point(center.X - width / 2, center.Y - height / 2);

        background.Width = width;
        background.Height = height;
        background.Location = FieldStart;
        background.BackColor = Color.Black;


        if (LevelForm.InvokeRequired)
        {
            LevelForm.Invoke(new Action(() => { LevelForm.Controls.Add(background); }));
        }
        else
        {
            LevelForm.Controls.Add(background);
        }
    }

    private void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (e is not IOEventArgs args)
        {
            throw new ArgumentException();
        }

        PictureBox pictureBox;
        if (PictureBoxes.ContainsKey(args.Entity))
        {
            pictureBox = PictureBoxes[args.Entity];
            pictureBox.Image = Image.FromFile(args.FilePath);
            return;
        }
        pictureBox = new PictureBox();
        PictureBoxes.Add(args.Entity, pictureBox);
        pictureBox.Width = TILE_WIDTH;
        pictureBox.Height = TILE_HEIGHT;
        pictureBox.Location = new Point(FieldStart.X + TILE_WIDTH * args.X,
                                        FieldStart.Y + TILE_HEIGHT * args.Y);
        pictureBox.Image = Image.FromFile(args.FilePath);
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

        if (LevelForm.InvokeRequired)
        {
            LevelForm.Invoke(new Action(() => { LevelForm.Controls.Add(pictureBox); }));
        }
        else
        {
            LevelForm.Controls.Add(pictureBox);
        }
        pictureBox.BringToFront();
    }

    private void HandleEntityMoved(object? sender, EventArgs e)
    {
        if (e is not IOEventArgs args)
        {
            throw new ArgumentException();
        }
        PictureBox pictureBox = PictureBoxes[args.Entity];
        pictureBox.Image = Image.FromFile(args.FilePath);
        pictureBox.Location = new Point(FieldStart.X + TILE_WIDTH * args.X,
                                    FieldStart.Y + TILE_HEIGHT * args.Y);
    }
    private void HandleEntityDeleted(object? sender, EventArgs e)
    {
        if (e is not IOEventArgs args)
        {
            throw new ArgumentException();
        }
        PictureBox pictureBox = PictureBoxes[args.Entity];
        PictureBoxes.Remove(args.Entity);

        if (LevelForm.InvokeRequired)
        {
            LevelForm.Invoke(new Action(() => { LevelForm.Controls.Remove(pictureBox); }));
        }
        else
        {
            LevelForm.Controls.Remove(pictureBox);
        }
        pictureBox.Dispose();
        pictureBox = null;
    }

    private void HandlePlayerStatsUpdated(object? sender, EventArgs e)
    {
        if (sender is not Player player)
            return;
        string res = "Player Health Points ";
        for (int i = 0; i < player.HealthPointsCurrent; i++)
            res += '\u2665';
        LevelForm.HealthTextBox.Text = res;
        LevelForm.ScoreTextBox.Text = $"Score: {player.Score}";
    }

    private void HandleSpawnTriggered(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn || e is not IOEventArgs ve)
            return;
        LevelForm.EventsTextBox.Text = $"{ve.Entity.GetName()} has appeared!";
    }

    private void HandleSpawnsCountUpdate(object? sender, EventArgs e)
    {
        if (sender is not Spawn spawn)
            return;
        string res = "Spawns Left: ";
        for (int i = 0; i < spawn.Field.EntitiesToSpawnCount; i++)
            res += '+';
        LevelForm.SpawnTextBox.Text = res;
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
        switch (menu.ExitStatus)
        {
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
        switch (scoreboard.ExitStatus)
        {
            case "startmenu":
                Game.StartMenu();
                break;
        }
    }
    public override string AskForInput()
    {
        return LevelForm.KeyCurrentlyPressed;
    }
    public override void ShowCongratulation()
    {
        throw new NotImplementedException();
    }
    public override void ShowLevelFinishedMessage(Field field)
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleCity;

public partial class ScoreboardForm : Form
{
    public ScoreboardForm(Game game, List<GameResult> gameResults, Size size, Point location)
    {
        Game = game;
        GameResults = gameResults;
        InitializeComponent();
        Size = size;
        Location = location;
        LoadData();
    }
    public string ExitStatus { get; set; }
    private Game Game { get; }
    private List<GameResult> GameResults { get; }
    private void LoadData()
    {
        ScoreboardTable.DataSource = GameResults;
    }
    private void ButtonBack_Click(object sender, EventArgs e)
    {
        ExitStatus = "startmenu";
        this.Close();
    }
    private void ScoreboardForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (ExitStatus is not null)
            return;
        DialogResult result = MessageBox.Show("Are you sure you want to close the application?",
            "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.No)
        {
            e.Cancel = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleCity;

public partial class LevelForm : Form
{
    public LevelForm()
    {
        InitializeComponent();
        this.WindowState = FormWindowState.Maximized;
        this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.UserPaint |
             ControlStyles.AllPaintingInWmPaint,
             true);
        this.UpdateStyles();
    }
    public string ExitStatus { get; set; }
    public string KeyCurrentlyPressed { get; set; }
    public TextBox LevelNameTextBox;
    public TextBox EventsTextBox;
    public TextBox SpawnTextBox;
    public TextBox HealthTextBox;
    public TextBox ScoreTextBox;
    private void LevelForm_FormClosing(object sender, FormClosingEventArgs e)
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

    private void LevelForm_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Up:
                KeyCurrentlyPressed = "up";
                break;
            case Keys.Down:
                KeyCurrentlyPressed = "down";
                break;
            case Keys.Left:
                KeyCurrentlyPressed = "left";
                break;
            case Keys.Right:
                KeyCurrentlyPressed = "right";
                break;
            case Keys.S:
                KeyCurrentlyPressed = "shoot";
                break;
            case Keys.Escape:
                KeyCurrentlyPressed = "escape";
                break;
            default:
                KeyCurrentlyPressed = e.KeyCode.ToString();
                break;
        }
    }
    private void LevelForm_KeyUp(object sender, KeyEventArgs e)
    {
        KeyCurrentlyPressed = "";
    }
}

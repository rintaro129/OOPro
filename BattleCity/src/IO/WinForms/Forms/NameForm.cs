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

public partial class NameForm : Form
{
    public NameForm(Game game, Size size, Point location)
    {
        InitializeComponent();
        Game = game;
        Location = location;
        Size = size;
    }
    public string ExitStatus;
    private Game Game { get; }

    private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            ExitStatus = "startmenu";
            Game.Name = NameTextBox.Text;
            this.Close();
        }
    }

    private void ConfirmButton_Click_1(object sender, EventArgs e)
    {
        ExitStatus = "startmenu";
        Game.Name = NameTextBox.Text;
        this.Close();
    }
    private void NameForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (ExitStatus == "startmenu")
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Enter the name.");
                e.Cancel = true;
            }
            return;
        }
        DialogResult result = MessageBox.Show("Are you sure you want to close the application?",
            "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.No)
        {
            e.Cancel = true;
        } else
        {
            Environment.Exit(0);
        }
    }
}

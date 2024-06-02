namespace BattleCity
{
    public partial class StartMenu : Form
    {
        public StartMenu(Game game, Size size, Point location)
        {
            Game = game;
            InitializeComponent();
            Size = size;
            Location = location;
        }
        public string ExitStatus;
        private Game Game { get; }

        private void scoreboard_Click(object sender, EventArgs e)
        {
            ExitStatus = "scoreboard";
            this.Close();
        }
        private void StartMenu_FormClosing(object sender, FormClosingEventArgs e)
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
}

namespace BattleCity
{
    partial class StartMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenu));
            Campaign = new Button();
            Random = new Button();
            Scoreboard = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Campaign
            // 
            Campaign.Anchor = AnchorStyles.None;
            Campaign.Location = new Point(213, 175);
            Campaign.Name = "Campaign";
            Campaign.Size = new Size(173, 30);
            Campaign.TabIndex = 0;
            Campaign.Text = "Campaign";
            Campaign.UseVisualStyleBackColor = true;
            Campaign.Click += Campaign_Click;
            // 
            // Random
            // 
            Random.Anchor = AnchorStyles.None;
            Random.Location = new Point(213, 247);
            Random.Name = "Random";
            Random.Size = new Size(173, 30);
            Random.TabIndex = 2;
            Random.Text = "Random";
            Random.UseVisualStyleBackColor = true;
            Random.Click += Random_Click;
            // 
            // Scoreboard
            // 
            Scoreboard.Anchor = AnchorStyles.None;
            Scoreboard.Location = new Point(213, 211);
            Scoreboard.Name = "Scoreboard";
            Scoreboard.Size = new Size(173, 30);
            Scoreboard.TabIndex = 1;
            Scoreboard.Text = "Scoreboard";
            Scoreboard.UseVisualStyleBackColor = true;
            Scoreboard.Click += Scoreboard_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(83, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(428, 157);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // StartMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(584, 361);
            Controls.Add(pictureBox1);
            Controls.Add(Scoreboard);
            Controls.Add(Random);
            Controls.Add(Campaign);
            MinimumSize = new Size(600, 400);
            Name = "StartMenu";
            StartPosition = FormStartPosition.Manual;
            Text = "BattleCity";
            FormClosing += StartMenu_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button Campaign;
        private Button Random;
        private Button Scoreboard;
        private PictureBox pictureBox1;
    }
}

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
            campaign = new Button();
            random = new Button();
            scoreboard = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // campaign
            // 
            campaign.Anchor = AnchorStyles.None;
            campaign.Location = new Point(213, 175);
            campaign.Name = "campaign";
            campaign.Size = new Size(173, 30);
            campaign.TabIndex = 0;
            campaign.Text = "Campaign";
            campaign.UseVisualStyleBackColor = true;
            // 
            // random
            // 
            random.Anchor = AnchorStyles.None;
            random.Location = new Point(213, 247);
            random.Name = "random";
            random.Size = new Size(173, 30);
            random.TabIndex = 2;
            random.Text = "Random";
            random.UseVisualStyleBackColor = true;
            // 
            // scoreboard
            // 
            scoreboard.Anchor = AnchorStyles.None;
            scoreboard.Location = new Point(213, 211);
            scoreboard.Name = "scoreboard";
            scoreboard.Size = new Size(173, 30);
            scoreboard.TabIndex = 1;
            scoreboard.Text = "Scoreboard";
            scoreboard.UseVisualStyleBackColor = true;
            scoreboard.Click += scoreboard_Click;
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
            Controls.Add(scoreboard);
            Controls.Add(random);
            Controls.Add(campaign);
            MinimumSize = new Size(600, 400);
            Name = "StartMenu";
            StartPosition = FormStartPosition.Manual;
            Text = "BattleCity";
            FormClosing += StartMenu_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button campaign;
        private Button random;
        private Button scoreboard;
        private PictureBox pictureBox1;
    }
}

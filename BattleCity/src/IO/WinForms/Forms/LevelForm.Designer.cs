namespace BattleCity;

partial class LevelForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelForm));
        SuspendLayout();
        // 
        // LevelForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(0, 0, 64);
        BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
        BackgroundImageLayout = ImageLayout.Center;
        ClientSize = new Size(1859, 919);
        Name = "LevelForm";
        Text = "BattleCity";
        FormClosing += LevelForm_FormClosing;
        KeyDown += LevelForm_KeyDown;
        KeyUp += LevelForm_KeyUp;
        ResumeLayout(false);
    }

    #endregion

    public void AddTextBoxes()
    {
        LevelNameTextBox = new TextBox();
        EventsTextBox = new TextBox();
        SpawnTextBox = new TextBox();
        HealthTextBox = new TextBox();
        ScoreTextBox = new TextBox();
        SuspendLayout();
        // 
        // LevelNameTextBox
        // 
        LevelNameTextBox.Enabled = false;
        LevelNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        LevelNameTextBox.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
        LevelNameTextBox.Location = new Point(1608, 270);
        LevelNameTextBox.Name = "LevelNameTextBox";
        LevelNameTextBox.ReadOnly = true;
        LevelNameTextBox.Size = new Size(239, 50);
        LevelNameTextBox.TabIndex = 0;
        LevelNameTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // EventsTextBox
        // 
        EventsTextBox.Enabled = false;
        EventsTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        EventsTextBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        EventsTextBox.Location = new Point(1608, 347);
        EventsTextBox.Name = "EventsTextBox";
        EventsTextBox.ReadOnly = true;
        EventsTextBox.Size = new Size(239, 29);
        EventsTextBox.TabIndex = 1;
        EventsTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // SpawnTextBox
        // 
        SpawnTextBox.Enabled = false;
        SpawnTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        SpawnTextBox.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        SpawnTextBox.Location = new Point(1608, 397);
        SpawnTextBox.Name = "SpawnTextBox";
        SpawnTextBox.ReadOnly = true;
        SpawnTextBox.Size = new Size(239, 35);
        SpawnTextBox.TabIndex = 2;
        SpawnTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // HealthTextBox
        // 
        HealthTextBox.Enabled = false;
        HealthTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        HealthTextBox.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        HealthTextBox.Location = new Point(1608, 455);
        HealthTextBox.Name = "HealthTextBox";
        HealthTextBox.ReadOnly = true;
        HealthTextBox.Size = new Size(239, 35);
        HealthTextBox.TabIndex = 3;
        HealthTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // ScoreTextBox
        // 
        ScoreTextBox.Enabled = false;
        ScoreTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        ScoreTextBox.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
        ScoreTextBox.Location = new Point(1608, 515);
        ScoreTextBox.Name = "ScoreTextBox";
        ScoreTextBox.ReadOnly = true;
        ScoreTextBox.Size = new Size(239, 50);
        ScoreTextBox.TabIndex = 4;
        ScoreTextBox.TextAlign = HorizontalAlignment.Center;

        Controls.Add(ScoreTextBox);
        Controls.Add(HealthTextBox);
        Controls.Add(SpawnTextBox);
        Controls.Add(EventsTextBox);
        Controls.Add(LevelNameTextBox);
    }
}
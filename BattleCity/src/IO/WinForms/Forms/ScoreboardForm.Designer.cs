namespace BattleCity;

partial class ScoreboardForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreboardForm));
        label1 = new Label();
        ScoreboardTable = new DataGridView();
        ButtonBack = new Button();
        ((System.ComponentModel.ISupportInitialize)ScoreboardTable).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        label1.AutoSize = true;
        label1.BackColor = Color.White;
        label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label1.Location = new Point(197, 100);
        label1.Name = "label1";
        label1.Size = new Size(183, 45);
        label1.TabIndex = 0;
        label1.Text = "Scoreboard";
        // 
        // ScoreboardTable
        // 
        ScoreboardTable.AllowUserToAddRows = false;
        ScoreboardTable.AllowUserToDeleteRows = false;
        ScoreboardTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        ScoreboardTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        ScoreboardTable.Location = new Point(69, 158);
        ScoreboardTable.MaximumSize = new Size(500, 700);
        ScoreboardTable.Name = "ScoreboardTable";
        ScoreboardTable.ReadOnly = true;
        ScoreboardTable.Size = new Size(444, 162);
        ScoreboardTable.TabIndex = 1;
        // 
        // ButtonBack
        // 
        ButtonBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        ButtonBack.BackColor = SystemColors.Control;
        ButtonBack.Location = new Point(497, 326);
        ButtonBack.Name = "ButtonBack";
        ButtonBack.Size = new Size(75, 23);
        ButtonBack.TabIndex = 2;
        ButtonBack.Text = "OK";
        ButtonBack.UseVisualStyleBackColor = false;
        ButtonBack.Click += ButtonBack_Click;
        // 
        // ScoreboardForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
        BackgroundImageLayout = ImageLayout.Center;
        ClientSize = new Size(584, 361);
        Controls.Add(ButtonBack);
        Controls.Add(ScoreboardTable);
        Controls.Add(label1);
        Name = "ScoreboardForm";
        StartPosition = FormStartPosition.Manual;
        Text = "BattleCity";
        FormClosing += ScoreboardForm_FormClosing;
        ((System.ComponentModel.ISupportInitialize)ScoreboardTable).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private DataGridView ScoreboardTable;
    private Button ButtonBack;
}
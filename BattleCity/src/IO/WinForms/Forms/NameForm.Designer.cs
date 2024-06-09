namespace BattleCity;

partial class NameForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameForm));
        label1 = new Label();
        ConfirmButton = new Button();
        NameTextBox = new TextBox();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.None;
        label1.AutoSize = true;
        label1.BackColor = Color.White;
        label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label1.Location = new Point(147, 67);
        label1.Name = "label1";
        label1.Size = new Size(290, 45);
        label1.TabIndex = 2;
        label1.Text = "What's your name?";
        // 
        // ConfirmButton
        // 
        ConfirmButton.Anchor = AnchorStyles.None;
        ConfirmButton.Location = new Point(255, 203);
        ConfirmButton.Name = "ConfirmButton";
        ConfirmButton.Size = new Size(75, 23);
        ConfirmButton.TabIndex = 1;
        ConfirmButton.Text = "Confirm";
        ConfirmButton.UseVisualStyleBackColor = true;
        ConfirmButton.Click += ConfirmButton_Click_1;
        // 
        // NameTextBox
        // 
        NameTextBox.Anchor = AnchorStyles.None;
        NameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        NameTextBox.Location = new Point(174, 150);
        NameTextBox.Name = "NameTextBox";
        NameTextBox.Size = new Size(238, 29);
        NameTextBox.TabIndex = 0;
        NameTextBox.TextAlign = HorizontalAlignment.Center;
        NameTextBox.KeyDown += NameTextBox_KeyDown;
        // 
        // NameForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
        BackgroundImageLayout = ImageLayout.Center;
        ClientSize = new Size(584, 361);
        Controls.Add(NameTextBox);
        Controls.Add(ConfirmButton);
        Controls.Add(label1);
        Name = "NameForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "BattleCity";
        FormClosing += NameForm_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Button ConfirmButton;
    private TextBox NameTextBox;
}
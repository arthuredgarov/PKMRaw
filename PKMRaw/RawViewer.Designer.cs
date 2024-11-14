namespace PKMRaw;

partial class RawViewer
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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(RawViewer));
        pkmRawTextBox = new TextBox();
        SuspendLayout();
        // 
        // pkmRawTextBox
        // 
        pkmRawTextBox.Location = new Point(12, 12);
        pkmRawTextBox.Multiline = true;
        pkmRawTextBox.Name = "pkmRawTextBox";
        pkmRawTextBox.ReadOnly = true;
        pkmRawTextBox.ScrollBars = ScrollBars.Vertical;
        pkmRawTextBox.Size = new Size(776, 426);
        pkmRawTextBox.TabIndex = 0;
        // 
        // RawViewer
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        ClientSize = new Size(800, 450);
        Controls.Add(pkmRawTextBox);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "RawViewer";
        ShowInTaskbar = false;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterScreen;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox pkmRawTextBox;
}
namespace PKMRaw;

/// <summary>
/// Partial class containing the Windows Forms designer generated code for the RawViewer form.
/// </summary>
partial class RawViewer
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// The search box text input control.
    /// </summary>
    private TextBox searchBox;

    /// <summary>
    /// The search button control.
    /// </summary>
    private Button searchButton;

    /// <summary>
    /// The label for the search box.
    /// </summary>
    private Label searchLabel;

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

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawViewer));

        // Search Label
        searchLabel = new Label();
        searchLabel.Location = new Point(12, 15);
        searchLabel.Size = new Size(50, 23);
        searchLabel.Text = "Search:";
        searchLabel.AutoSize = true;

        // Search Box
        searchBox = new TextBox();
        searchBox.Location = new Point(65, 12);
        searchBox.Size = new Size(200, 23);
        searchBox.Name = "searchBox";
        searchBox.KeyPress += SearchBox_KeyPress;

        // Search Button
        searchButton = new Button();
        searchButton.Location = new Point(270, 11);
        searchButton.Size = new Size(75, 25);
        searchButton.Name = "searchButton";
        searchButton.Text = "Search";
        searchButton.Click += SearchButton_Click;

        // PKM Raw TextBox - adjusted position
        pkmRawTextBox = new TextBox();
        pkmRawTextBox.Location = new Point(12, 45);
        pkmRawTextBox.Multiline = true;
        pkmRawTextBox.Name = "pkmRawTextBox";
        pkmRawTextBox.ReadOnly = true;
        pkmRawTextBox.ScrollBars = ScrollBars.Vertical;
        pkmRawTextBox.Size = new Size(776, 393);
        pkmRawTextBox.TabIndex = 0;

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        ClientSize = new Size(800, 450);
        Controls.Add(searchLabel);
        Controls.Add(searchBox);
        Controls.Add(searchButton);
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

    /// <summary>
    /// The text box that displays the raw Pokemon data.
    /// </summary>
    private TextBox pkmRawTextBox;
}
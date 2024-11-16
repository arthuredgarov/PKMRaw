using PKHeX.Core;
using PKMRaw.Extensions;
using System.Text;

namespace PKMRaw;

/// <summary>
/// A form that displays Pokemon data in a raw JSON format with search capabilities.
/// </summary>
public partial class RawViewer : Form
{
    /// <summary>
    /// Stores the original JSON text before any search filtering.
    /// </summary>
    private readonly string _originalText;

    /// <summary>
    /// Initializes a new instance of the <see cref="RawViewer"/> class.
    /// </summary>
    /// <param name="pokeMon">The Pokemon data to display in the viewer.</param>
    public RawViewer(PKM pokeMon)
    {
        InitializeComponent();

        Text = GetWindowText(pokeMon);

        _originalText = pokeMon.ToJsonString();
        pkmRawTextBox.Text = _originalText;
    }

    /// <summary>
    /// Handles the search button click event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An EventArgs that contains no event data.</param>
    private void SearchButton_Click(object sender, EventArgs e)
    {
        PerformSearch();
    }

    /// <summary>
    /// Handles the key press event in the search box.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            PerformSearch();
        }
    }

    /// <summary>
    /// Performs the search operation on the Pokemon data.
    /// </summary>
    private void PerformSearch()
    {
        var searchTerm = searchBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            pkmRawTextBox.Text = _originalText;
            return;
        }

        try
        {
            // Split the text into lines
            var lines = _originalText.Split('\n');
            var matchedLines = new List<string>
            {
                // Add opening brace
                "{"
            };

            // Search through each line
            for (var i = 1; i < lines.Length - 1; i++)  // Skip first and last braces
            {
                if (lines[i].Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    var line = lines[i].Trim();

                    // Add comma if not the last matched line
                    if (!line.EndsWith(','))
                        line += ",";

                    matchedLines.Add("  " + line);
                }
            }

            // Remove trailing comma from last line if present
            if (matchedLines.Count > 1)
            {
                var lastLine = matchedLines[^1];
                if (lastLine.EndsWith(','))
                    matchedLines[^1] = lastLine.TrimEnd(',');
            }

            // Add closing brace
            matchedLines.Add("}");

            // Update the textbox with matched lines
            pkmRawTextBox.Text = string.Join(Environment.NewLine, matchedLines);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Search error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #region Private methods
    private static string GetWindowText(PKM pokeMon)
    {
        var builder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(pokeMon.Nickname))
            builder.Append(pokeMon.Nickname).Append(' ');

        return builder.Append(pokeMon.PID).ToString();
    }
    #endregion
}
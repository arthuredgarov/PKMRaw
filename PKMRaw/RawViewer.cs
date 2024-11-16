using PKHeX.Core;
using System.Text;
using System.Reflection;

namespace PKMRaw;

/// <summary>
/// A form that displays Pokemon data in a raw JSON format with search capabilities.
/// </summary>
public partial class RawViewer : Form
{
    /// <summary>
    /// Stores the original JSON text before any search filtering.
    /// </summary>
    private readonly string originalText;

    /// <summary>
    /// Initializes a new instance of the <see cref="RawViewer"/> class.
    /// </summary>
    /// <param name="pokéMon">The Pokemon data to display in the viewer.</param>
    public RawViewer(PKM pokéMon)
    {
        InitializeComponent();
        Text = GetWindowText(pokéMon);
        originalText = SerializePKM(pokéMon);
        pkmRawTextBox.Text = originalText;
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
        string searchTerm = searchBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            pkmRawTextBox.Text = originalText;
            return;
        }

        try
        {
            // Split the text into lines
            string[] lines = originalText.Split('\n');
            List<string> matchedLines = new List<string>();

            // Add opening brace
            matchedLines.Add("{");

            // Search through each line
            for (int i = 1; i < lines.Length - 1; i++)  // Skip first and last braces
            {
                if (lines[i].Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    string line = lines[i].Trim();
                    // Add comma if not the last matched line
                    if (!line.EndsWith(","))
                        line += ",";
                    matchedLines.Add("  " + line);
                }
            }

            // Remove trailing comma from last line if present
            if (matchedLines.Count > 1)
            {
                string lastLine = matchedLines[matchedLines.Count - 1];
                if (lastLine.EndsWith(","))
                    matchedLines[matchedLines.Count - 1] = lastLine.TrimEnd(',');
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
    private static string GetWindowText(PKM pokéMon)
    {
        var builder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(pokéMon.Nickname))
            builder.Append(pokéMon.Nickname).Append(' ');
        builder.Append(pokéMon.PID);
        return builder.ToString();
    }

    private static string SerializePKM(PKM pkm)
    {
        var builder = new StringBuilder();
        builder.AppendLine("{");

        // Get all readable properties
        var properties = pkm.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead);

        var lastProp = properties.Last();
        foreach (var prop in properties)
        {
            try
            {
                var value = prop.GetValue(pkm);
                var valueStr = FormatValue(value, prop.PropertyType);

                builder.Append($"  \"{prop.Name}\": {valueStr}");

                // Add comma if not the last property
                if (prop != lastProp)
                    builder.AppendLine(",");
                else
                    builder.AppendLine();
            }
            catch
            {
                // Skip properties that can't be read or serialized
                continue;
            }
        }

        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string FormatValue(object? value, Type propertyType)
    {
        if (value == null)
            return "null";

        // Handle span types
        if (propertyType == typeof(ReadOnlySpan<byte>))
            return "[bytes...]";
        if (propertyType == typeof(ReadOnlySpan<ushort>))
            return "[ushorts...]";

        return value switch
        {
            string str => $"\"{str}\"",
            bool b => b.ToString().ToLowerInvariant(),
            DateTime dt => $"\"{dt:yyyy-MM-dd HH:mm:ss}\"",
            IEnumerable<byte> bytes => $"[{string.Join(", ", bytes)}]",
            Array arr => $"[{string.Join(", ", arr.Cast<object>())}]",
            _ => value.ToString() ?? "null"
        };
    }
    #endregion
}
using Newtonsoft.Json;
using PKHeX.Core;
using System.Text;

namespace PKMRaw;

/// <inheritdoc/>
public partial class RawViewer : Form
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RawViewer"/> class.
    /// </summary>
    /// <param name="pokéMon">
    /// The input <see cref="PKM"/> instance
    /// to be shown in JSON format.
    /// </param>
    public RawViewer(PKM pokéMon)
    {
        InitializeComponent();

        Text = GetWindowText(pokéMon);

        pkmRawTextBox.Text = JsonConvert.SerializeObject(pokéMon, Formatting.Indented);
    }
    #endregion

    #region Private methods
    private static string GetWindowText(PKM pokéMon)
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(pokéMon.Nickname))
            builder.Append(pokéMon.Nickname).Append(' ');

        builder.Append(pokéMon.PID);

        return builder.ToString();
    }
    #endregion
}
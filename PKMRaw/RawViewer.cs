using PKHeX.Core;
using PKMRaw.Extensions;
using System.Text;

namespace PKMRaw;

/// <inheritdoc/>
public partial class RawViewer : Form
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RawViewer"/> class.
    /// </summary>
    /// <param name="pokeMon">
    /// The input <see cref="PKM"/> instance
    /// to be shown in JSON format.
    /// </param>
    public RawViewer(PKM pokeMon)
    {
        InitializeComponent();
        Text = GetWindowText(pokeMon);
        pkmRawTextBox.Text = pokeMon.ToJsonString();
    }
    #endregion

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
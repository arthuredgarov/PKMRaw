using PKHeX.Core;
using System.Diagnostics.CodeAnalysis;

namespace PKMRaw;

/// <summary>
/// PKM Raw plugin implementation.
/// </summary>
public sealed class PkmRawPlugin : IPlugin
{
    #region Constants
    private const string MissingMenuErrorMessage = "Main menu was not passed into the plugin initializer.";

    private const string PluginName = "PKM Raw";
    private const int PluginPriority = 1;
    #endregion

    #region Properties
    /// <inheritdoc/>
    public string Name { get; } = PluginName;

    /// <inheritdoc/>
    public int Priority { get; } = PluginPriority;

    /// <inheritdoc/>
    public ISaveFileProvider SaveFileEditor { get; } = null!;

    /// <summary>
    /// Currently viewed PokeMon instance.
    /// </summary>
    [NotNull]
    public IPKMView? Pkm { get; private set; }
    #endregion

    #region Public methods
    /// <inheritdoc/>
    [MemberNotNull(nameof(Pkm))]
    public void Initialize(params object[] args)
    {
        Pkm = GetArgumentOfType<IPKMView>(args);

        var menu = GetArgumentOfType<ToolStrip>(args);

        AddPluginControls(menu);
    }

    /// <inheritdoc/>
    public void NotifySaveLoaded() { }

    /// <inheritdoc/>
    public bool TryLoadFile(string filePath)
        => false;
    #endregion

    #region Private methods
    private void AddPluginControls(ToolStrip menu)
    {
        if (menu.Items.Find("Menu_Tools", false)[0] is not ToolStripDropDownItem tools)
        {
            MessageBox.Show(MissingMenuErrorMessage, Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        tools.DropDownItems.Add(new ToolStripMenuItem("View raw PKM data", Resources.PluginIcon.ToBitmap(), ViewPokeMon));
    }

    private void ViewPokeMon(object? sender, EventArgs e)
        => new RawViewer(Pkm.Data).Show();

    private static T GetArgumentOfType<T>(params object[] args)
        => args.FirstOrDefault(arg => arg is T) is T result
        ? result
        : throw new InvalidOperationException($"{typeof(T).Name} was not provided.");
    #endregion
}
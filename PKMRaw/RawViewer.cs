using PKHeX.Core;
using System.Text;
using System.Reflection;

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
        pkmRawTextBox.Text = SerializePKM(pokéMon);
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
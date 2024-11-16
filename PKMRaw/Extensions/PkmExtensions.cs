using PKHeX.Core;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace PKMRaw.Extensions;

internal static class PkmExtensions
{
    #region Constants
    private const char LeftCurlyBrace = '{';
    private const char RightCurlyBrace = '}';

    private const string EmptyObjectString = "{}";
    private const string NullString = "\"null\"";
    #endregion

    #region Fields
    private static readonly JsonSerializerOptions _defaultJsonOptions = new()
    {
        WriteIndented = true
    };
    #endregion

    #region Public methods
    public static string ToJsonString(this PKM pokeMon)
    {
        var json = pokeMon.JsonSerializeWithReflectionFallback();

        // Doing additional deserialize-serialize circle to return beautified JSON string.
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);

        return JsonSerializer.Serialize(jsonElement, _defaultJsonOptions);
    }
    #endregion

    #region Private methods
    private static string JsonSerializeWithReflectionFallback(this object? input, bool isRootObject = true)
    {
        try
        {
            var json = JsonSerializer.Serialize(input);

            return json;
        }
        catch { }

        if (input is null)
        {
            return isRootObject
                ? EmptyObjectString
                : NullString;
        }

        var builder = new StringBuilder();
        builder.Append(LeftCurlyBrace);

        foreach (var property in input.GetProperties())
        {
            var propertyValue = input.GetPropertyValueOrNull(property).JsonSerializeWithReflectionFallback(false);
            var propertyName = property.Name;

            builder.Append($"\"{propertyName}\":{propertyValue},");
        }

        // Remove trailing comma
        builder.Remove(builder.Length - 1, 1);

        builder.Append(RightCurlyBrace);

        return builder.ToString();
    }

    private static IEnumerable<PropertyInfo> GetProperties(this object @object)
    {
        var type = @object.GetType();

        var publicProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return publicProperties.Where(property => property.CanRead).DistinctBy(property => property.Name);
    }

    private static object? GetPropertyValueOrNull(this object input, PropertyInfo property)
    {
        try
        {
            var value = property.GetMethod?.Invoke(input, []);

            return value;
        }
        catch
        {
            return null;
        }
    }
    #endregion
}
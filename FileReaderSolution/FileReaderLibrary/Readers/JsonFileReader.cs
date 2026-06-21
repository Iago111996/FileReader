using FileReaderLibrary.Interfaces;
using System.Text.Json;

namespace FileReaderLibrary.Readers;

/// <summary>
/// Reads and pretty-prints a JSON file.
/// </summary>
public class JsonFileReader : IFileReader
{
    /// <summary>
    /// Reads the content of a JSON file and returns it as a pretty-printed string.
    /// </summary>
    /// <param name="filePath">The path to the JSON file to read.</param>
    /// <returns>A pretty-printed JSON string.</returns>
    public string Read(string filePath)
    {
        string raw = File.ReadAllText(filePath);
        using JsonDocument doc = JsonDocument.Parse(raw);
        return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
    }
}
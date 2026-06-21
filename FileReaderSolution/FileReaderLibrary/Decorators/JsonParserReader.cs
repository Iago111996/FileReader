using FileReaderLibrary.Interfaces;
using System.Text.Json;

namespace FileReaderLibrary.Decorators;

/// <summary>
/// Decorator that parses JSON from the string returned by an inner reader,
/// using <see cref="JsonDocument.Parse(string)"/> instead of reading directly from the file path.
/// This allows the inner reader to pre-process the content (e.g. decrypt) before JSON parsing.
/// </summary>
public class JsonParserReader : IFileReader
{
    private readonly IFileReader _fileReader;

    /// <summary>
    /// Initializes a new instance wrapping the given reader.
    /// </summary>
    /// <param name="fileReader">The inner file reader to wrap.</param>
    public JsonParserReader(IFileReader fileReader) => _fileReader = fileReader;

    /// <inheritdoc />
    public string Read(string filePath)
    {
        string jsonContent = _fileReader.Read(filePath);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
    }
}

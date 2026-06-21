using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Readers;

/// <summary>
/// Reads plain text files using <see cref="File.ReadAllText(string)"/>.
/// </summary>
public class TextFileReader : IFileReader
{
    /// <inheritdoc />
    public string Read(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}   

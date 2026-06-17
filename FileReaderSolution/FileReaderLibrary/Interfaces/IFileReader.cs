namespace FileReaderLibrary.Interfaces;

/// <summary>
/// Defines a contract for reading the contents of a file.
/// </summary>
public interface IFileReader
{
    /// <summary>
    /// Reads the full content of the specified file and returns it as a string.
    /// </summary>
    /// <param name="filePath">The absolute or relative path to the file.</param>
    /// <returns>The file contents as a string.</returns>
    string Read(string filePath);
}

namespace FileReaderLibrary.Interfaces;

/// <summary>
/// Defines a contract for validating a file before it is read.
/// </summary>
public interface IFileValidator
{
    /// <summary>
    /// Validates the specified file. Throws an exception if validation fails.
    /// </summary>
    /// <param name="filePath">The absolute or relative path to the file.</param>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file does not exist.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown when the file fails a validation rule.</exception>
    void ValidateFile(string filePath);
}

using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Validations;

/// <summary>
/// Validates that a file is not empty (size greater than zero bytes).
/// </summary>
public class FileNotEmptyValidator : IFileValidator
{
    /// <inheritdoc />
    public void ValidateFile(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        if (fileInfo.Length == 0)
        {
            throw new InvalidOperationException($"File is empty: {filePath}");
        }
    }
}

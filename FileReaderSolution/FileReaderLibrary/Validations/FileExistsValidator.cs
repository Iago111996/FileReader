using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Validations;

/// <summary>
/// Validates that a file exists and, optionally, that its extension is in an allowed list.
/// </summary>
public class FileExistsValidator : IFileValidator
{
    private readonly HashSet<string> _allowedExtensions;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="allowedExtensions">Optional list of allowed extensions (e.g. ".txt"). If null or empty, all extensions are accepted.</param>
    public FileExistsValidator(IEnumerable<string>? allowedExtensions = null)
    {
        _allowedExtensions = allowedExtensions != null
            ? new HashSet<string>(allowedExtensions, StringComparer.OrdinalIgnoreCase)
            : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public void ValidateFile(string filePath)
    {
        if(!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        if(_allowedExtensions.Count > 0)
        {
            var fileExtension = Path.GetExtension(filePath);
            if (!_allowedExtensions.Contains(fileExtension))
                throw new InvalidOperationException($"File extension '{fileExtension}' is not allowed."); 
        }
    }
}

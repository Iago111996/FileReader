using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Decorators;

/// <summary>
/// Decorator that validates a file before delegating the read to an inner <see cref="IFileReader"/>.
/// </summary>
public class ValidatedFileReader : IFileReader
{
    private readonly IFileReader _fileReader;
    private readonly IFileValidator _fileValidator;

    /// <summary>Initializes a new instance wrapping the given reader and validator.</summary>
    public ValidatedFileReader(IFileReader fileReader, IFileValidator fileValidator)
    {
        _fileReader = fileReader;
        _fileValidator = fileValidator;
    }

    /// <inheritdoc />
    public string Read(string filePath)
    {
        _fileValidator.ValidateFile(filePath);
        return _fileReader.Read(filePath);
    }
}

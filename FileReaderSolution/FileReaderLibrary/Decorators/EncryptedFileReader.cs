using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Decorators;

/// <summary>
/// Decorator that decrypts the content of a file after reading it with an inner <see cref="IFileReader"/>.
/// </summary>
public class EncryptedFileReader : IFileReader
{
    private readonly IFileReader _fileReader;
    private readonly IEncryptionAlgorithm _encryptionAlgorithm;

    /// <summary>
    /// Initializes a new instance wrapping the given reader and encryption algorithm.
    /// </summary>
    /// <param name="fileReader"></param>
    /// <param name="encryptionAlgorithm"></param>
    public EncryptedFileReader(IFileReader fileReader, IEncryptionAlgorithm encryptionAlgorithm)
    {
        _fileReader = fileReader;
        _encryptionAlgorithm = encryptionAlgorithm;
    }

    /// <inheritdoc />
    public string Read(string filePath)
    {
        string encryptedContent = _fileReader.Read(filePath);
        return _encryptionAlgorithm.Decrypt(encryptedContent);
    }
}

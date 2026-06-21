using FileReaderLibrary.Decorators;
using FileReaderLibrary.Enums;
using FileReaderLibrary.Interfaces;
using FileReaderLibrary.Readers;
using FileReaderLibrary.Validations;
using Microsoft.VisualBasic.FileIO;

namespace FileReaderLibrary;

/// <summary>
/// Fluent builder for constructing an <see cref="Interfaces.IFileReader"/> with optional validation.
/// </summary>
public class FileReaderBuilder
{
    private IFileValidator? _validator;
    private IEncryptionAlgorithm? _encryptionAlgorithm;
    private EFileType _fileType = EFileType.Text;
    private static readonly HashSet<EFileType> EncryptionSupportedTypes = new() { EFileType.Text };

    /// <summary>
    /// Adds an extra <see cref="IFileValidator"/> to run in addition to the default validators.
    /// </summary>
    /// <param name="validator">The validator to add.</param>
    /// <returns>The current <see cref="FileReaderBuilder"/> instance.</returns>
    public FileReaderBuilder WithValidation(IFileValidator validator)
    {
        _validator = validator;
        return this;
    }

    /// <summary>
    /// Sets the encryption algorithm to be used for decrypting the file content. This is optional and can be set if the file content is encrypted.
    /// </summary>
    /// <param name="encryptionAlgorithm">The encryption algorithm to use.</param>
    /// <returns>The current <see cref="FileReaderBuilder"/> instance.</returns>
    public FileReaderBuilder WithEncryption(IEncryptionAlgorithm encryptionAlgorithm)
    {
        _encryptionAlgorithm = encryptionAlgorithm;
        return this;
    }   

    /// <summary>
    /// Sets the file type for the reader. Currently, this is not used in the builder but can be extended in the future to support different file types.
    /// </summary>
    /// <param name="fileType"></param>
    /// <returns></returns>
    public FileReaderBuilder WithFileType(EFileType fileType)
    {
        _fileType = fileType;
        return this;
    }

    /// <summary>
    /// Builds the <see cref="IFileReader"/> with the default validators (file exists, file not empty)
    /// plus any extra validator added via <see cref="WithValidation"/>.
    /// </summary>
    public IFileReader Build()
    {
        IFileReader reader = _fileType switch
        {
            EFileType.Text => new TextFileReader(),
            EFileType.Xml => new XmlFileReader(),
            _ => throw new NotSupportedException($"File type {_fileType} is not supported.")
        };

        if (_encryptionAlgorithm is not null)
        {
            if (!EncryptionSupportedTypes.Contains(_fileType))
                throw new NotSupportedException(
                    $"Encrypted reading is not supported for '{_fileType}' files.");
            reader = new EncryptedFileReader(reader, _encryptionAlgorithm);
        }

        var validators = new List<IFileValidator>
        {
            new FileExistsValidator(),
            new FileNotEmptyValidator()
        };

        if (_validator is not null)
            validators.Add(_validator);

        reader = new ValidatedFileReader(reader, new CompositeValidator(validators));

        return reader;
    }
}

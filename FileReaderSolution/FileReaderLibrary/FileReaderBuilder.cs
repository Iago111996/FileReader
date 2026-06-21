using FileReaderLibrary.Decorators;
using FileReaderLibrary.Enums;
using FileReaderLibrary.Interfaces;
using FileReaderLibrary.Readers;
using FileReaderLibrary.Validations;

namespace FileReaderLibrary;

/// <summary>
/// Fluent builder for constructing an <see cref="Interfaces.IFileReader"/> with optional validation.
/// </summary>
public class FileReaderBuilder
{
    private IFileValidator? _validator;
    private IEncryptionAlgorithm? _encryptionAlgorithm;
    private ISecurityContext? _securityContext;
    private string _role = string.Empty;
    private EFileType _fileType = EFileType.Text;
    private static readonly HashSet<EFileType> SecuritySupportedTypes = new() { EFileType.Text, EFileType.Xml };

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
    /// Sets the security context and role for role-based access control. This is optional and can be set if the file reading requires security checks.
    /// </summary>
    /// <param name="securityContext">The security context to use.</param>
    /// <param name="role">The role to use for access control.</param>
    /// <returns>The current <see cref="FileReaderBuilder"/> instance.</returns>
    public FileReaderBuilder WithSecurity(ISecurityContext securityContext, string role)
    {
        _securityContext = securityContext;
        _role = role;
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
        IFileReader reader;

        if (_fileType == EFileType.Xml && _encryptionAlgorithm is not null)
        {
            reader = new XmlParserReader(
                new EncryptedFileReader(
                    new TextFileReader(),         
                    _encryptionAlgorithm));       
                                                  
        }
        else
        {
            reader = _fileType switch
            {
                EFileType.Text => new TextFileReader(),
                EFileType.Xml => new XmlFileReader(),
                EFileType.Json => new JsonFileReader(),
                _ => throw new NotSupportedException($"File type {_fileType} is not supported.")
            };

            if (_encryptionAlgorithm is not null)
                reader = new EncryptedFileReader(reader, _encryptionAlgorithm);
        }

        if (_securityContext is not null)
        {
            if (!SecuritySupportedTypes.Contains(_fileType))
                throw new NotSupportedException(
                    $"Role-based security is not supported for '{_fileType}' files.");
            reader = new SecureFileReader(reader, _securityContext, _role);
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

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
    private EFileType _fileType = EFileType.Text;

    /// <summary>
    /// Adds an extra <see cref="IFileValidator"/> to run in addition to the default validators.
    /// </summary>
    public FileReaderBuilder WithValidation(IFileValidator validator)
    {
        _validator = validator;
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

using FileReaderLibrary.Decorators;
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

    /// <summary>
    /// Adds an extra <see cref="IFileValidator"/> to run in addition to the default validators.
    /// </summary>
    public FileReaderBuilder WithValidation(IFileValidator validator)
    {
        _validator = validator;
        return this;
    }

    /// <summary>
    /// Builds the <see cref="IFileReader"/> with the default validators (file exists, file not empty)
    /// plus any extra validator added via <see cref="WithValidation"/>.
    /// </summary>
    public IFileReader Build()
    {
        IFileReader reader = new TextFileReader();
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

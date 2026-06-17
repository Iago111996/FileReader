using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Decorators;

/// <summary>
/// Runs multiple <see cref="IFileValidator"/> instances sequentially.
/// Validation stops and throws on the first failure.
/// </summary>
public class CompositeValidator : IFileValidator
{
    private readonly IReadOnlyList<IFileValidator> _validators;

    /// <summary>Initializes a new instance with the given validators.</summary>
    public CompositeValidator(IEnumerable<IFileValidator> validators)
    {
        _validators = validators.ToList();
    }

    /// <inheritdoc />
    public void ValidateFile(string filePath)
    {
        foreach (var validator in _validators)
            validator.ValidateFile(filePath);
    }
}

using FileReaderLibrary.Enums;
using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Decorators;

/// <summary>
/// Decorator that checks if the current user has permission to read a file before delegating the read to an inner <see cref="IFileReader"/>.
/// </summary>
public class SecureFileReader : IFileReader
{
    private readonly IFileReader _fileReader;
    private readonly ISecurityContext _securityContext;
    private readonly string _role;

    /// <summary>
    /// Initializes a new instance wrapping the given reader, security context, and user role.
    /// </summary>
    /// <param name="fileReader"></param>
    /// <param name="securityContext"></param>
    /// <param name="role"></param>
    public SecureFileReader(IFileReader fileReader, ISecurityContext securityContext, string role)
    {
        _fileReader = fileReader;
        _securityContext = securityContext;
        _role = role;
    }

    /// <summary>
    /// Reads the content of a file if the current user has permission; otherwise, throws an <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>The content of the file.</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown if the user does not have permission to read the file.</exception>
    public string Read(string filePath)
    {
        if (!_securityContext.CanReadFile(filePath, _role))
            throw new UnauthorizedAccessException(
                $"Role '{_role}' does not have permission to read file '{filePath}'.");

        return _fileReader.Read(filePath);
    }
}

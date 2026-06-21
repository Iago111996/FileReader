namespace FileReaderLibrary.Interfaces;

/// <summary>
/// Defines a contract for a security context that determines whether a user with a specific role can read a given file.
/// </summary>
public interface ISecurityContext
{
    /// <summary>
    /// Determines whether a user with the specified role has permission to read the file at the given path.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    bool CanReadFile(string filePath, string role);
}

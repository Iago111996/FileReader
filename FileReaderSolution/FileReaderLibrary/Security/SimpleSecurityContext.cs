using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Security;

/// <summary>
/// A simple implementation of <see cref="ISecurityContext"/> that checks if a user with a given role can read a specific file based on predefined permissions.
/// </summary>
public class SimpleSecurityContext : ISecurityContext
{
    /// <summary>
    /// A dictionary that maps roles to their allowed file permissions. The key is the role name, and the value is a list of file names that the role can access. A wildcard "*" indicates that the role has access to all files.
    /// </summary>
    private readonly Dictionary<string, List<string>> _rolePermissions = new()
    {
        { "Admin", new List<string> { "*" } }, 
        { "Manager", new List<string> { "relatorio.xml", "vendas.xml", "relatorio.txt", "vendas.txt", "relatorio.json", "vendas.json" } }, 
        { "User", new List<string> { "perfil.xml", "perfil.txt", "perfil.json" } }
    };

    /// <summary>
    /// Checks if a user with the specified role can read the given file based on the predefined permissions.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool CanReadFile(string filePath, string role)
    {
        if (!_rolePermissions.ContainsKey(role))
            return false;

        var permissions = _rolePermissions[role];

        if (permissions.Contains("*"))
            return true;

        string fileName = Path.GetFileName(filePath);
        return permissions.Contains(fileName);
    }
}

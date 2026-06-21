namespace FileReaderLibrary.Interfaces;

/// <summary>
/// Defines a contract for encryption algorithms that can decrypt content.
/// </summary>
public interface IEncryptionAlgorithm
{
    /// <summary>
    /// Decrypts the given encrypted content and returns the original content.
    /// </summary>
    /// <param name="encryptedContent"></param>
    /// <returns></returns>
    string Decrypt(string encryptedContent);
}

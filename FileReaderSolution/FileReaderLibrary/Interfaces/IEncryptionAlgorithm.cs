namespace FileReaderLibrary.Interfaces;

/// <summary>
/// Defines a contract for symmetric encryption algorithms that can encrypt and decrypt content.
/// </summary>
public interface IEncryptionAlgorithm
{
    /// <summary>
    /// Encrypts the given plain content and returns the encrypted form.
    /// </summary>
    /// <param name="plainContent">The plain text to encrypt.</param>
    /// <returns>The encrypted content.</returns>
    string Encrypt(string plainContent);

    /// <summary>
    /// Decrypts the given encrypted content and returns the original content.
    /// </summary>
    /// <param name="encryptedContent">The encrypted content to decrypt.</param>
    /// <returns>The decrypted content.</returns>
    string Decrypt(string encryptedContent);
}

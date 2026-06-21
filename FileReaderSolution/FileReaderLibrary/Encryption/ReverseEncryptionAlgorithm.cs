using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Encryption;

/// <summary>
/// A simple encryption algorithm that reverses the string. This is for demonstration purposes only and should not be used for real encryption needs.
/// </summary>
public class ReverseEncryptionAlgorithm : IEncryptionAlgorithm
{
    /// <summary>
    /// Decrypts the given encrypted content by reversing the string back to its original form.
    /// </summary>
    /// <param name="encryptedContent">The content to decrypt.</param>
    /// <returns>The decrypted content.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the encrypted content is null.</exception>
    public string Decrypt(string encryptedContent)
    {
        if (encryptedContent == null)
        {
            throw new ArgumentNullException(nameof(encryptedContent));
        }

        char[] charArray = encryptedContent.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}

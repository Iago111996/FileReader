using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary.Encryption;

/// <summary>
/// A simple encryption algorithm that reverses the string. This is for demonstration purposes only and should not be used for real encryption needs.
/// </summary>
public class ReverseEncryptionAlgorithm : IEncryptionAlgorithm
{
    /// <inheritdoc />
    public string Encrypt(string plainContent)
    {
        ArgumentNullException.ThrowIfNull(plainContent);

        char[] charArray = plainContent.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <inheritdoc />
    public string Decrypt(string encryptedContent)
    {
        ArgumentNullException.ThrowIfNull(encryptedContent);

        char[] charArray = encryptedContent.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}

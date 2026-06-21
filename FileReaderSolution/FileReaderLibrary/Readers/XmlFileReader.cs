using FileReaderLibrary.Interfaces;
using System.Xml.Linq;

namespace FileReaderLibrary.Readers;

/// <summary>
/// Reads XML files using <see cref="XDocument.Load(string)"/>.
/// </summary>
public class XmlFileReader : IFileReader
{
    /// <inheritdoc />
    public string Read(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        return doc.ToString();
    }
}

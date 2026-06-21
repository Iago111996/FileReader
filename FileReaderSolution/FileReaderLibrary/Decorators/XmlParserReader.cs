using FileReaderLibrary.Interfaces;
using System.Xml.Linq;

namespace FileReaderLibrary.Decorators;

/// <summary>
/// Decorator that parses XML from the string returned by an inner reader,
/// using XDocument.Parse() instead of XDocument.Load(filePath).
/// This allows the inner reader to decrypt the content before parsing.
/// </summary>
public class XmlParserReader : IFileReader
{
    private readonly IFileReader _fileReader;

    /// <summary>
    /// Initializes a new instance wrapping the given reader.
    /// </summary>
    /// <param name="fileReader">The inner file reader to wrap.</param>
    public XmlParserReader(IFileReader fileReader) => _fileReader = fileReader;

    /// <inheritdoc />
    public string Read(string filePath)
    {
        string xmlContent = _fileReader.Read(filePath);
        XDocument doc = XDocument.Parse(xmlContent); 
        return doc.ToString();
    }
}
using FileReaderLibrary;
using FileReaderLibrary.Interfaces;

IFileReader reader = new FileReaderBuilder()
    .WithFileType(FileReaderLibrary.Enums.EFileType.Xml)
    //.WithEncryption(new FileReaderLibrary.Encryption.ReverseEncryptionAlgorithm())   
    .WithSecurity(new FileReaderLibrary.Security.SimpleSecurityContext(), "User")
    .Build();

string content = reader.Read("C:\\Users\\ipp\\Desktop\\DOC\\exame\\perfil.xml");
Console.WriteLine(content);
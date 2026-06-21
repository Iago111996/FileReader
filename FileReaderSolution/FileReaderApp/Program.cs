using FileReaderLibrary;
using FileReaderLibrary.Interfaces;

IFileReader reader = new FileReaderBuilder()
    .WithFileType(FileReaderLibrary.Enums.EFileType.Json)
    .WithEncryption(new FileReaderLibrary.Encryption.ReverseEncryptionAlgorithm())
    //.WithSecurity(new FileReaderLibrary.Security.SimpleSecurityContext(), "User")
    .Build();

string content = reader.Read("C:\\Users\\ipp\\Desktop\\DOC\\exame\\invoice_encrypted.json");
Console.WriteLine(content);
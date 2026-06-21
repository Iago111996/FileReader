using FileReaderLibrary;
using FileReaderLibrary.Interfaces;

IFileReader reader = new FileReaderBuilder()
    .WithFileType(FileReaderLibrary.Enums.EFileType.Text)
    .WithEncryption(new FileReaderLibrary.Encryption.ReverseEncryptionAlgorithm())   
    .Build();

string content = reader.Read("C:\\Users\\ipp\\Desktop\\DOC\\exame\\encrypted_sample.txt");
Console.WriteLine(content);
using FileReaderLibrary;
using FileReaderLibrary.Interfaces;

IFileReader reader = new FileReaderBuilder()
    .WithFileType(FileReaderLibrary.Enums.EFileType.Xml)
    .Build();

string content = reader.Read("C:\\Users\\ipp\\Desktop\\DOC\\exame\\sample.xml");
Console.WriteLine(content);
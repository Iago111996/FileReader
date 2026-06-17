using FileReaderLibrary;
using FileReaderLibrary.Interfaces;

IFileReader reader = new FileReaderBuilder()
    .Build();

string content = reader.Read("C:\\Users\\ipp\\Desktop\\DOC\\exame\\sample.txt");
Console.WriteLine(content);
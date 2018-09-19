using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.IO.Abstractions;

public class AbstractLoggerTests
{
    IFileSystem fileSystem;
    FileBase file;
    DirectoryBase directory;

    LogLevel logLevel;
    string locationSource;
    string message;

    [SetUp]
    public void Init()
    {
        fileSystem = Substitute.For<IFileSystem>();
        file = Substitute.For<FileBase>();
        directory = Substitute.For<DirectoryBase>();
        fileSystem.File.Returns(file);
        fileSystem.Directory.Returns(directory);

        logLevel = LogLevel.ALL;
        message = "Message";
    }

    [Test]
    public void Append_all_text_when_log_called()
    {
        file.Exists(Arg.Any<string>()).Returns(true);
        directory.Exists(Arg.Any<string>()).Returns(true);

        var sut = new DevLogger(fileSystem);

        sut.Log(logLevel, message);

        fileSystem.File.Received(1).AppendAllText(Arg.Any<string>(), Arg.Any<string>());
    }

    [Test]
    public void Create_file_and_directory_when_they_do_not_exist()
    {
        file.Exists(Arg.Any<string>()).Returns(false);
        directory.Exists(Arg.Any<string>()).Returns(false);

        Stream stream = Substitute.For<Stream>();
        file.Create(Arg.Any<string>()).Returns(stream);

        var sut = new DevLogger(fileSystem);

        sut.Log(logLevel, message);

        fileSystem.Directory.Received(1).CreateDirectory(Arg.Any<string>());
        fileSystem.File.Received(1).Create(Arg.Any<string>());
    }
}

using System;
using System.IO.Abstractions;

public sealed class DevLogger : AbstractLogger
{
    protected override string FilePath => $"{ DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") }.txt";
    protected override string DirectoryPath => "DeveloperLogs";

    public DevLogger() : base()
    {
    }

    public DevLogger(IFileSystem fileSystem) : base(fileSystem)
    {
    }

    public override void Log(LogLevel logLevel, string message)
    {
        WriteToFile(logLevel, message);
    }
}
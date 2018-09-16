using System;

public sealed class DevLogger : AbstractLogger
{
    protected override string FilePath => $"{ DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") }.txt";
    protected override string DirectoryPath => "DeveloperLogs";

    public override void Log(LogLevel logLevel, string locationSource, string message)
    {
        WriteToFile(logLevel, locationSource, message);
    }
}
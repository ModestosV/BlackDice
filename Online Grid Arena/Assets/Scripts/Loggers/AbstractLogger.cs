using System;
using System.IO;
using System.IO.Abstractions;
using UnityEngine;

public abstract class AbstractLogger : ILogger
{
    protected abstract string FilePath { get; }
    protected abstract string DirectoryPath { get; }

    private readonly IFileSystem fileSystem;

    protected AbstractLogger() : this(
        fileSystem: new FileSystem()
    )
    {

    }

    protected AbstractLogger(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem;
    }
    

    public abstract void Log(LogLevel logLevel, string message);

    protected void WriteToFile(
        LogLevel logLevel,
        string message,
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = ""
    )
    {
        string fullFilePath = Path.Combine(DirectoryPath, FilePath);

        try
        {
            if (!fileSystem.Directory.Exists(DirectoryPath))
            {
                fileSystem.Directory.CreateDirectory(DirectoryPath);
            }

            if (!fileSystem.File.Exists(fullFilePath))
            {
                fileSystem.File.Create(fullFilePath).Dispose();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        fileSystem.File.AppendAllText(fullFilePath, $"{ DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") } -- " +
            $"Source: { sourceFilePath } -> { memberName } -- [{ logLevel }]: { message }");
    }
}


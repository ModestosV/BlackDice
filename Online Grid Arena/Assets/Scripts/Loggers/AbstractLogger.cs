using System;
using System.IO;
using System.IO.Abstractions;
using UnityEngine;

public abstract class AbstractLogger : ILogger
{
    protected abstract string FilePath { get; }
    protected abstract string DirectoryPath { get; }

    readonly IFileSystem fileSystem;

    protected AbstractLogger() : this(
        fileSystem: new FileSystem()
    )
    {

    }

    protected AbstractLogger(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem;
    }

    public abstract void Log(LogLevel logLevel, string locationSource, string message);

    protected void WriteToFile(LogLevel logLevel, string locationSource, string message)
    {
        string file = Path.Combine(DirectoryPath, FilePath);

        try
        {
            if (!fileSystem.Directory.Exists(DirectoryPath))
            {
                fileSystem.Directory.CreateDirectory(DirectoryPath);
            }

            if (!fileSystem.File.Exists(file))
            {
                fileSystem.File.Create(file).Dispose();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        StreamWriter writer = new StreamWriter(file, true);
        writer.WriteLine($"{ DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") } -- " +
            $"Source: {locationSource} -- [{logLevel}]: { message }");
        writer.Flush();
    }
}


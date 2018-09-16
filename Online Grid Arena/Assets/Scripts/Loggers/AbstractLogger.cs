using System;
using System.IO;
using UnityEngine;

public abstract class AbstractLogger : ILogger
{
    protected abstract string FilePath { get; }
    protected abstract string DirectoryPath { get; }

    public abstract void Log(LogLevel logLevel, string locationSource, string message);

    protected void WriteToFile(LogLevel logLevel, string locationSource, string message)
    {
        string file = Path.Combine(DirectoryPath, FilePath);

        try
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            if (!File.Exists(file))
            {
                File.Create(file).Dispose();
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


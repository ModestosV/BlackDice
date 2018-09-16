using System;
using System.IO;
using UnityEngine;

public abstract class AbstractLogger : ILogger
{
    abstract protected string FilePath { get; }
    abstract protected string DirectoryPath { get; }

    public abstract void Log(string message);

    protected void WriteToFile(string text)
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
        writer.WriteLine($"{ DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") }: { text }");
        writer.Flush();
    }
}


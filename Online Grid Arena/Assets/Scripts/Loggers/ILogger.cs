interface ILogger
{
    void Log(LogLevel logLevel, string message);
}

public enum LogLevel
{
    FATAL = 0,
    ERROR = 1,
    WARN = 2,
    INFO = 3,
    DEBUG = 4,
    TRACE = 5,
    ALL = 6,
    OFF = 7
}
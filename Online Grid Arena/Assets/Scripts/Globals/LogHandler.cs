using UnityEngine;

public sealed class LogHandler
{
    private readonly LogglyNetworkManager logglyNetworkManager;

    public LogHandler()
    {
        Application.logMessageReceived += HandleLogAsync;
        logglyNetworkManager = new LogglyNetworkManager();
    }

    private async void HandleLogAsync(string logString, string stackTrace, LogType type)
    {
        LogDTO logDto = new LogDTO(type.ToString(), logString, stackTrace);
        await logglyNetworkManager.SendLog(logDto);
    }
}

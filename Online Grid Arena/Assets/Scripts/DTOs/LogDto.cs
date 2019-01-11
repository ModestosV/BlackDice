using Newtonsoft.Json;
using UnityEngine;

public sealed class LogDTO
{
    [JsonProperty("LEVEL")]
    public string Level { get; }
    [JsonProperty("Message")]
    public string Message { get; }
    [JsonProperty("Stack_Trace")]
    public string StackTrace { get; }
    [JsonProperty("Device_Model")]
    public string DeviceModel { get; }

    public LogDTO(string Level, string Message, string StackTrace)
    {
        this.Level = Level;
        this.Message = Message;
        this.StackTrace = StackTrace;
        DeviceModel = SystemInfo.deviceModel;
    }
}
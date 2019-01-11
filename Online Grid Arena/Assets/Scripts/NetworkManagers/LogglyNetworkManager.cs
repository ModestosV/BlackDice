using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public sealed class LogglyNetworkManager : AbstractNetworkManager
{
    public LogglyNetworkManager() : base(URLs.URL_LOGGLY) { }

    public async Task<IHttpResponseMessage> SendLog(LogDTO logDto)
    {
        HttpResponseMessage response = await PostAsync("", JsonConvert.SerializeObject(logDto));
        return new HttpResponseMessageAdapter(response);
    }
}
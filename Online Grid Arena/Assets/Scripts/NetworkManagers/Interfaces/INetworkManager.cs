using System.Net.Http;
using System.Threading.Tasks;

public interface INetworkManager
{
    Task<HttpResponseMessage> PostAsync(string targetRequestUrl, string messageBody);
}

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

public abstract class AbstractNetworkManager : INetworkManager
{
    private readonly string endpoint;
    private readonly HttpClient client;

    protected AbstractNetworkManager(string endpoint) : this(endpoint, HttpClientService.Instance)
    {
    }

    protected AbstractNetworkManager(string endpoint, HttpClient client)
    {
        this.endpoint = endpoint;
        this.client = client;
    }

    public async Task<HttpResponseMessage> PostAsync(string targetRequestUrl, string messageBody)
    {
       HttpResponseMessage response = null;
        try
        {
            response = await client.PostAsync(endpoint + targetRequestUrl, new StringContent(messageBody, Encoding.UTF8, "application/json"));
        }
        catch (HttpRequestException)
        {
            response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        return response;
    }
}

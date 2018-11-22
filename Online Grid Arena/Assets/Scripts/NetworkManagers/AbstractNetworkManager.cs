using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public abstract class AbstractNetworkManager : IHttpRequests
{
    protected readonly string mainURL;
    private readonly HttpClient client;

    protected AbstractNetworkManager(string extensionURL) : this(extensionURL, HttpClientService.Instance)
    {
    }

    protected AbstractNetworkManager(string extensionURL, HttpClient client)
    {
        mainURL = URLs.BASE_URL + extensionURL;
        this.client = client;
    }

    public async Task<HttpResponseMessage> PostAsync(string url, string body)
    {
        return await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
    }
}

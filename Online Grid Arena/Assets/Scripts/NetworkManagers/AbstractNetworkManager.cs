using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public abstract class AbstractNetworkManager : IHttpRequests
{
    public IOnlineMenuPanel Panel { get; set; }

    protected readonly string mainURL;

    protected AbstractNetworkManager(string extensionURL)
    {
        mainURL = URLs.BASE_URL + "/" + extensionURL;
    }

    public async Task<HttpResponseMessage> PostAsync(string url, string body)
    {
        using (var client = new HttpClient())
        {
            return await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
        }
    }
}

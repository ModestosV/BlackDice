using System.Net.Http;
using System.Threading.Tasks;

public interface IHttpRequests
{
    Task<HttpResponseMessage> PostAsync(string url, string body);
}

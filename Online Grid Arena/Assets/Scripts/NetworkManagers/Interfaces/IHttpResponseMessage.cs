using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public interface IHttpResponseMessage
{
    Version Version { get; set; }
    HttpContent Content { get; set; }
    HttpStatusCode StatusCode { get; set; }
    string ReasonPhrase { get; set; }
    HttpResponseHeaders Headers { get; }
    HttpRequestMessage RequestMessage { get; set; }
    bool IsSuccessStatusCode { get; }

    void Dispose();
    HttpResponseMessage EnsureSuccessStatusCode();
    string ToString();

    Task<string> ReadContentAsStringAsync();
}

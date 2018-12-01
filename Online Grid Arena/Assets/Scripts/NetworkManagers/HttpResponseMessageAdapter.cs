using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class HttpResponseMessageAdapter : IHttpResponseMessage
{
    private HttpResponseMessage Adaptee { get; set; }

    public HttpResponseMessageAdapter(HttpResponseMessage adaptee)
    {
        Adaptee = adaptee;
    }

    public Version Version { get { return Adaptee.Version; } set { Adaptee.Version = value; } }

    public HttpContent Content { get { return Adaptee.Content; } set { Adaptee.Content = value; } }
    public HttpStatusCode StatusCode { get { return Adaptee.StatusCode; } set { Adaptee.StatusCode = value; } }
    public string ReasonPhrase { get { return Adaptee.ReasonPhrase; } set { Adaptee.ReasonPhrase = value; } }

    public HttpResponseHeaders Headers { get { return Adaptee.Headers; } }

    public HttpRequestMessage RequestMessage { get { return Adaptee.RequestMessage; } set { Adaptee.RequestMessage = value; } }

    public bool IsSuccessStatusCode { get { return Adaptee.IsSuccessStatusCode; } }


    public void Dispose()
    {
        Adaptee.Dispose();
    }

    public HttpResponseMessage EnsureSuccessStatusCode()
    {
        return Adaptee.EnsureSuccessStatusCode();
    }

    public Task<string> ReadContentAsStringAsync()
    {
        return Adaptee.Content.ReadAsStringAsync();
    }
}

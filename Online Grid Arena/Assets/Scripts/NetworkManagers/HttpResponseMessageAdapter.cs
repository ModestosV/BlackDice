using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public sealed class HttpResponseMessageAdapter : IHttpResponseMessage
{
    private readonly HttpResponseMessage adaptee;

    public HttpResponseMessageAdapter(HttpResponseMessage adaptee)
    {
        this.adaptee = adaptee;
    }

    public Version Version { get => adaptee.Version; set => adaptee.Version = value; }
    public HttpContent Content { get => adaptee.Content; set => adaptee.Content = value; }
    public HttpStatusCode StatusCode { get => adaptee.StatusCode; set => adaptee.StatusCode = value; }
    public string ReasonPhrase { get => adaptee.ReasonPhrase; set => adaptee.ReasonPhrase = value; }
    public HttpResponseHeaders Headers => adaptee.Headers;
    public HttpRequestMessage RequestMessage { get => adaptee.RequestMessage; set => adaptee.RequestMessage = value; }
    public bool IsSuccessStatusCode => adaptee.IsSuccessStatusCode;

    public void Dispose()
    {
        adaptee.Dispose();
    }

    public HttpResponseMessage EnsureSuccessStatusCode()
    {
        return adaptee.EnsureSuccessStatusCode();
    }

    public Task<string> ReadContentAsStringAsync()
    {
        return adaptee.Content.ReadAsStringAsync();
    }
}

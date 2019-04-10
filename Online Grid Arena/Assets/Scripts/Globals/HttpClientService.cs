using System;
using System.Net.Http;

/// <summary>
///  Singleton for HttpClient, based on code found at: http://csharpindepth.com/Articles/General/Singleton.aspx
/// </summary>
public sealed class HttpClientService
{
    private static readonly Lazy<HttpClient> Lazy = new Lazy<HttpClient>(() => new HttpClient());

    public static HttpClient Instance => Lazy.Value;

    private HttpClientService() { }
}


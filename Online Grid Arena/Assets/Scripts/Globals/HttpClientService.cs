using System;
using System.Net.Http;

/// <summary>
///  Singleton for HttpClient, based on cound found at: http://csharpindepth.com/Articles/General/Singleton.aspx
/// </summary>
public sealed class HttpClientService
{
    private static readonly Lazy<HttpClient> lazy = new Lazy<HttpClient>(() => new HttpClient());

    public static HttpClient Instance { get { return lazy.Value; } }

    private HttpClientService() { }
}


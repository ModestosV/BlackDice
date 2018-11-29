﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine;

public abstract class AbstractNetworkManager : INetworkManager
{
    private readonly string endpoint;
    private readonly HttpClient client;

    protected AbstractNetworkManager(string extensionURL) : this(extensionURL, HttpClientService.Instance)
    {
    }

    protected AbstractNetworkManager(string extensionURL, HttpClient client)
    {
        endpoint = URLs.BASE_URL + extensionURL;
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

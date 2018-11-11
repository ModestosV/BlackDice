using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class WebRequester
{
    protected UnityWebRequest www;

    protected IEnumerator Get(string url, Action<long, string> callback)
    {
        www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            callback(www.responseCode, www.downloadHandler.text);
        }
    }

    protected IEnumerator Post(string url, WWWForm body, Action<long, string> callback)
    {
        www = UnityWebRequest.Post(url, body);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            callback(www.responseCode, www.downloadHandler.text);
        }
    }
}
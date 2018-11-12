using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class WebRequester : MonoBehaviour
{
    protected UnityWebRequest www;

    protected void Get(string url, Action callback)
    {
        StartCoroutine(url, callback);
    }

    private IEnumerator GetCoroutine(string url, Action callback)
    {
        www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        callback();
    }

    protected void Post(string url, WWWForm body, Action callback)
    {
        StartCoroutine(PostCoroutine(url, body, callback));
    }

    private IEnumerator PostCoroutine(string url, WWWForm body, Action callback)
    {
        www = UnityWebRequest.Post(url, body);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        callback();
    }
}
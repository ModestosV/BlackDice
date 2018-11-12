using UnityEngine.Networking;

public class WebResponse : IWebResponse
{
    protected UnityWebRequest Www { get; set; }

    public bool IsNetworkError { get { return Www.isNetworkError; } }
    public bool IsHttpError { get { return Www.isHttpError; } }
    public long ResponseCode { get { return Www.responseCode; } }
    public string ResponseText { get { return Www.downloadHandler.text; } }

    public WebResponse(UnityWebRequest www)
    {
        Www = www;
    }

}

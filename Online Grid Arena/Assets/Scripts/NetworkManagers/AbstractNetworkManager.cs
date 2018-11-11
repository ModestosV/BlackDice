using System.Collections;
using System.Text;
using UnityEngine.Networking;

public abstract class AbstractNetworkManager : IHttpRequests
{
    public IOnlineMenuPanel Panel { get; set; }

    protected readonly string mainURL;

    protected AbstractNetworkManager(string extensionURL)
    {
        mainURL = URLs.BASE_URL + "/" + extensionURL;
    }

    public IEnumerator Post(string url, string body)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Panel.GetStatus(request.responseCode.ToString());
    }
}

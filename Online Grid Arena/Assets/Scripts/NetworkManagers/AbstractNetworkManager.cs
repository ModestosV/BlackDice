using System.Collections;
using System.Text;
using UnityEngine.Networking;

public abstract class AbstractNetworkManager : IHttpRequests
{
    public IPanel Panel { get; set; }

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

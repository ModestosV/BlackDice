using System.Collections;
using System.Text;
using UnityEngine.Networking;

public abstract class AbstractNetworkManager : IHttpRequests
{
    public string Data { get; set; }
    public string StatusCode { get; set; }
    public IPanel Panel { get; set; }

    public IEnumerator Post(string url, string body)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        StatusCode = request.responseCode.ToString();
        Panel.GetStatus(this);
        //while (request.responseCode == 0){ }
        //Data = request.downloadHandler.text;
        //yield return StatusCode = request.responseCode.ToString();
    }
}

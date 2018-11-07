using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbstractNetworkManager : IHttpRequests
{
    public string Data { get; set; }
    public string StatusCode { get; set; }

    public IEnumerator Post(string url, string body)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();
        while (request.responseCode == 0){ }
        yield return StatusCode = request.responseCode.ToString();
    }
}

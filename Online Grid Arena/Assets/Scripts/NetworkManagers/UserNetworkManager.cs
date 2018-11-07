using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class UserNetworkManager
{
    private string baseUrl = "http://localhost:5500/account";

    // TODO: Do JSON stuff better + remove Debug.Logs
    public IEnumerator CreateUser(UserDto userDto)
    {
        var request = new UnityWebRequest(baseUrl + "/register", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes($"{{\"password\":\"{userDto.PasswordHash}\",\"email\":\"{userDto.Email}\",\"username\":\"{userDto.Username}\"}}");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log(bodyRaw.ToString());
        Debug.Log("Response Body: " + request.downloadHandler.text);
        Debug.Log("Response Code: " + request.responseCode);
        
    }
}
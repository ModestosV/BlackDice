using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserNetworkManager
{
    private WWWForm Form { get; set; }
    private string baseUrl = "http://localhost:5500/account";

    public UserNetworkManager(): this(new WWWForm())
    {

    }

    public UserNetworkManager( WWWForm Form)
    {
        this.Form = Form;
    }

    public IEnumerator CreateUser(UserDto userDto)
    {
        Form.AddField("createdAt", userDto.CreatedAt.ToString());
        Form.AddField("email", userDto.Email);
        Form.AddField("givenName", userDto.GivenName);
        Form.AddField("loggedIn", userDto.LoggedIn.ToString());
        Form.AddField("passwordHash", userDto.PasswordHash);
        Form.AddField("surname", userDto.Surname);
        Form.AddField("username", userDto.Username);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/register", Form))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            
            var marc = www.downloadHandler.text;
            Debug.Log(marc);
        }
    }
}
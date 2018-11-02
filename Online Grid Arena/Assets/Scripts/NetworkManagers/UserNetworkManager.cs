using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UserNetworkManager
{
    // TODO: Figure out if we'll use properties (probably yes if await/async, no if using/yield"
    private UnityWebRequest Www { get; set; }
    private WWWForm Form { get; set; }
    private string baseUrl = "http://localhost:5500/account";

    public UserNetworkManager(): this(new UnityWebRequest(), new WWWForm())
    {

    }

    public UserNetworkManager(UnityWebRequest Www, WWWForm Form)
    {
        this.Www = Www;
        this.Form = Form;
    }

    // TODO: Make this return a boolean?
    public IEnumerator CreateUser(UserDto userDto)
    {
        Form.AddField("createdAt", userDto.CreatedAt.ToString());
        Form.AddField("email", userDto.Email);
        Form.AddField("givenName", userDto.GivenName);
        Form.AddField("loggedIn", userDto.LoggedIn.ToString());
        Form.AddField("passwordHash", userDto.PasswordHash);
        Form.AddField("surname", userDto.Surname);
        Form.AddField("username", userDto.Username);


        // TODO: Look into UnityWebRequestAsyncOperation
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/register", Form))
        {
            yield return www.SendWebRequest();

            // Todo: Actually handle the JSON Object
            var marc = www.downloadHandler.text;

            if(marc.Contains("200"))
            {
                yield return true;
            }
            else
            {
                yield return false;
            }
        }
    }
}
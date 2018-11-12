using Newtonsoft.Json;
using UnityEngine;

public class AccountDTO
{
    [JsonProperty("email")]
    public string Email { get; }
    [JsonProperty("loggedInToken")]
    public string LoggedInToken { get; }
    [JsonProperty("password")]
    public string PasswordHash { get; }
    [JsonProperty("username")]
    public string Username { get; }

    public AccountDTO(string Email, string PasswordHash, string Username = "", string LoggedInToken = "")
    {
        this.Email = Email;
        this.PasswordHash = PasswordHash;
        this.Username = Username;
        this.LoggedInToken = LoggedInToken;
    }

    public WWWForm GetForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", Email);
        form.AddField("password", PasswordHash);
        form.AddField("username", Username);
        form.AddField("loggedIn", LoggedInToken);

        return form;
    }
}
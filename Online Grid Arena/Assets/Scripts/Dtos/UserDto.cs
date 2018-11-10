
using Newtonsoft.Json;
using System;

public class UserDTO
{
    [JsonProperty("email")]
    public string Email { get; }
    [JsonProperty("loggedIn")]
    public bool LoggedIn { get; }
    [JsonProperty("password")]
    public string PasswordHash { get; }
    [JsonProperty("username")]
    public string Username { get; }

    public UserDTO
    (
        string Email,
        string PasswordHash,
        string Username = "",
        bool LoggedIn = false
    )
    {
        this.Email = Email;
        this.LoggedIn = LoggedIn;
        this.PasswordHash = PasswordHash;
        this.Username = Username;
    }
}
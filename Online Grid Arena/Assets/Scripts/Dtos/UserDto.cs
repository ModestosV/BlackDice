
using Newtonsoft.Json;
using System;

public class UserDTO
{
    [JsonProperty("email")]
    public string Email { get; }
    [JsonProperty("loggedInToken")]
    public string LoggedInToken { get; }
    [JsonProperty("password")]
    public string PasswordHash { get; }
    [JsonProperty("username")]
    public string Username { get; }

    public UserDTO
    (
        string Email,
        string PasswordHash,
        string Username,
        string LoggedInToken = ""
    )
    {
        this.Email = Email;
        this.LoggedInToken = LoggedInToken;
        this.PasswordHash = PasswordHash;
        this.Username = Username;
    }
}
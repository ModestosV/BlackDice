
using System;

public class UserDTO
{
    public DateTime CreatedAt { get; }
    public string Email { get; }
    public string GivenName { get; }
    public bool LoggedIn { get; }
    public string PasswordHash { get; }
    public string Surname { get; }
    public string Username { get; }

    public UserDTO
    (
        string Email,
        string PasswordHash,
        string Username = "",
        DateTime CreatedAt = new DateTime(),
        bool LoggedIn = false,
        string Surname = "",
        string GivenName = ""
    )
    {
        this.CreatedAt = CreatedAt;
        this.Email = Email;
        this.GivenName = GivenName;
        this.LoggedIn = LoggedIn;
        this.PasswordHash = PasswordHash;
        this.Surname = Surname;
        this.Username = Username;
    }
}
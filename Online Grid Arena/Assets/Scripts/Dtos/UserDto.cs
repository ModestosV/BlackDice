using System;

public class UserDto
{
    public DateTime CreatedAt { get; }
    public string Email { get; }
    public string GivenName { get; }
    public bool LoggedIn { get; }
    public string PasswordHash { get; }
    public string Surname { get; }
    public string Username { get; }

    public UserDto
    (
        string Email,
        string Username,
        string PasswordHash,
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


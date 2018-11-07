using System;

public class UserDto
{
    public DateTime createdAt { get; }
    public string email { get; }
    public bool loggedIn { get; }
    public string passwordHash { get; }
    public string username { get; }

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
        this.createdAt = CreatedAt;
        this.email = Email;
        this.loggedIn = LoggedIn;
        this.passwordHash = PasswordHash;
        this.username = Username;
    }
}


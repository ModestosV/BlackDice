using System;

public class UserDto
{
    private DateTime CreatedAt { get; }
    private string Email { get; }
    private string GivenName { get; }
    private bool LoggedIn { get; }
    private string PasswordHash { get; }
    private string Surname { get; }
    private string Username { get; }

    public UserDto
    (
        DateTime CreatedAt,
        string Email,
        string Username,
        string PasswordHash,
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


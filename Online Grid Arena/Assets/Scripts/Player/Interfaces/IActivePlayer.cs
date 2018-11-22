
public interface IActivePlayer
{
    string Email { get; }
    
    UserDTO LoggedInUser { get;  set; }

    bool IsLoggedIn();
    void Logout();
}

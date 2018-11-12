
public interface IUserController
{
    string Email { get; }

    IOnlineMenuController OnlineMenuController { set; }

    void Register(UserDTO user);
    void InvalidRegistration();
    void ErroredRegistration();
    void Login(UserDTO user);
    void InvalidLogin();
    void ErroredLogin();
    void Logout(UserDTO user);
    void InvalidLogout();
    void ErroredLogout();

    bool IsLoggedIn();
}

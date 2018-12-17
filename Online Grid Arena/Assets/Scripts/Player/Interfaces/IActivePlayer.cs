public interface IActivePlayer
{
    UserDTO LoggedInUser { get; set; }
    bool IsLoggedIn();
    void Logout();
}

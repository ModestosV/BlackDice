public interface IActivePlayer
{
    UserDto LoggedInUser { get; set; }
    bool IsLoggedIn();
    void Logout();
}

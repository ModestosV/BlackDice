public class ActivePlayer : IActivePlayer
{
    public UserDTO LoggedInUser { get; set; }

    public bool IsLoggedIn()
    {
        return LoggedInUser != null;
    }

    public void Logout()
    {
        LoggedInUser = null;
    }
}
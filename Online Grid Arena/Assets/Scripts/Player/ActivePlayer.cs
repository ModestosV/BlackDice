public class ActivePlayer : IActivePlayer
{
    public string Email
    {
        get
        {
            return LoggedInUser == null ?  "" : LoggedInUser.Email;
        }
    }

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
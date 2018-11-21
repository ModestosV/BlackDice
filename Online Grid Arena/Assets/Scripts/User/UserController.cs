public class UserController : IUserController
{
    public string Email
    {
        get
        {
            if (LoggedInUser == null) return "";
            return LoggedInUser.Email;
        }
    }

    public UserDTO LoggedInUser { protected get; set; }
    

    public bool IsLoggedIn()
    {
        return LoggedInUser != null;
    }
}

public interface IUserController
{
    string Email { get; }
    
    UserDTO LoggedInUser { set; }

    bool IsLoggedIn();
}

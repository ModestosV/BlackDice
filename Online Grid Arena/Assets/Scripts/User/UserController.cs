public class UserController : IUserController
{
    public IOnlineMenuController OnlineMenuController { protected get; set; }
    public string Email
    {
        get
        {
            if (Account == null) return "";
            return Account.Email;
        }
    }

    public UserDTO Account { protected get; set; }

    public void InvalidRegistration()
    {
        OnlineMenuController.SetRegistrationStatus(Strings.INVALID_REQUEST_DUPLICATE_KEYS);
        OnlineMenuController.EnableRegisterButton();
    }

    public void ErroredRegistration()
    {
        OnlineMenuController.SetRegistrationStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        OnlineMenuController.EnableRegisterButton();
    }

    public void InvalidLogin()
    {
        OnlineMenuController.SetLoginStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        OnlineMenuController.EnableLoginLogoutButtons();
    }

    public void ErroredLogin()
    {
        OnlineMenuController.SetLoginStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        OnlineMenuController.EnableLoginLogoutButtons();
    }

    public void InvalidLogout()
    {
        OnlineMenuController.SetLoginStatus(Strings.LOGOUT_FAIL_MESSAGE);
        OnlineMenuController.EnableLoginLogoutButtons();
    }

    public void ErroredLogout()
    {
        OnlineMenuController.SetLoginStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        OnlineMenuController.EnableLoginLogoutButtons();
    }

    public void Login(UserDTO user)
    {
        Account = user;
        OnlineMenuController.SetLoginStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}\n Welcome {user.Username}.");
        OnlineMenuController.ToggleLoginLogoutButtons();
        OnlineMenuController.EnableLoginLogoutButtons();
    }

    public void Logout(UserDTO user)
    {
        Account = null;
        OnlineMenuController.SetLoginStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
        OnlineMenuController.ToggleLoginLogoutButtons();
        OnlineMenuController.EnableLoginLogoutButtons();
    }

    public void Register(UserDTO user)
    {
        OnlineMenuController.SetRegistrationStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
        OnlineMenuController.EnableRegisterButton();
    }

    public bool IsLoggedIn()
    {
        return Account != null;
    }
}
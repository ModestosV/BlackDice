public class AccountController : IAccountController
{
    public IOnlineMenuController OnlineMenuController { protected get; set; }

    public AccountDTO Account { protected get; set; }

    public void InvalidRegistration()
    {
        OnlineMenuController.SetRegistrationStatus(Strings.INVALID_REGISTRATION_REQUEST);
    }

    public void ErroredRegistration()
    {
        OnlineMenuController.SetRegistrationStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    public void InvalidLogin()
    {
        OnlineMenuController.SetLoginStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
    }

    public void ErroredLogin()
    {
        OnlineMenuController.SetLoginStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    public void InvalidLogout()
    {
        OnlineMenuController.SetLoginStatus(Strings.LOGOUT_FAIL_MESSAGE);
    }

    public void ErroredLogout()
    {
        OnlineMenuController.SetLoginStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    public void Login(AccountDTO user)
    {
        Account = user;
        OnlineMenuController.SetLoginStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}\n Welcome {user.Username}.");
        OnlineMenuController.ToggleLoginLogoutButtons();
    }

    public void Logout(AccountDTO user)
    {
        Account = null;
        OnlineMenuController.SetLoginStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
        OnlineMenuController.ToggleLoginLogoutButtons();
    }

    public void Register(AccountDTO user)
    {
        OnlineMenuController.SetRegistrationStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
    }
}
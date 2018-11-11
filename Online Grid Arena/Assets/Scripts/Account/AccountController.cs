public class AccountController : IAccountController
{
    public IOnlineMenuPanel RegistrationPanel { protected get; set; }
    public IOnlineMenuPanel LoginPanel { protected get; set; }

    public AccountDTO Account { protected get; set; }

    public void InvalidRegistration()
    {
        RegistrationPanel.SetStatus(Strings.INVALID_REGISTRATION_REQUEST);
    }

    public void ErroredRegistration()
    {
        RegistrationPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    public void InvalidLogin()
    {
        LoginPanel.SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
    }

    public void ErroredLogin()
    {
        LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    public void InvalidLogout()
    {
        LoginPanel.SetStatus(Strings.LOGOUT_FAIL_MESSAGE);
    }

    public void ErroredLogout()
    {
        LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
    }

    public void Login(AccountDTO user)
    {
        Account = user;
        LoginPanel.SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE} Welcome {user.Username}");
    }

    public void Logout(AccountDTO user)
    {
        Account = null;
        LoginPanel.SetStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
    }

    public void Register(AccountDTO user)
    {
        RegistrationPanel.SetStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
    }
}
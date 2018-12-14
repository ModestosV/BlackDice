using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine;

public sealed class OnlineMenuController : IOnlineMenuController
{
    public IRegistrationPanel RegistrationPanel { private get; set; }
    public ILoginPanel LoginPanel { private get; set; }
    public IUserNetworkManager UserNetworkManager { private get; set; }
    public IActivePlayer ActivePlayer { private get; set; }

    public async void Register(string email, string password, string username)
    {
        if (!ValidateEmail(email))
        {
            RegistrationPanel.SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(password))
        {
            RegistrationPanel.SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
            return;
        }

        if (!ValidateUsername(username))
        {
            RegistrationPanel.SetStatus(Strings.INVALID_USERNAME_MESSAGE);
            return;
        }

        RegistrationPanel.DisableRegisterButton();
        RegistrationPanel.ActivateLoadingCircle();
        RegistrationPanel.ClearStatus();

        IHttpResponseMessage response = await UserNetworkManager.CreateUserAsync(new UserDTO(email, Hash128.Compute(password).ToString(), username));

        RegistrationPanel.EnableRegisterButton();
        RegistrationPanel.DeactivateLoadingCircle();

        switch ((int)response.StatusCode)
        {
            case 201:
                RegistrationPanel.SetStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
                break;
            case 400:
                RegistrationPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
                break;
            case 412:
                RegistrationPanel.SetStatus(Strings.INVALID_REQUEST_DUPLICATE_KEYS);
                break;
            case 500:
                RegistrationPanel.SetStatus(Strings.SERVER_ERROR_MESSAGE);
                break;
        }
    }

    public async void Login(string email, string password)
    {
        if (ActivePlayer.IsLoggedIn()) return;

        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();

        UserDTO user = new UserDTO(email, Hash128.Compute(password).ToString());

        IHttpResponseMessage response = await UserNetworkManager.LoginAsync(user);

        LoginPanel.EnableLoginLogoutButtons();
        LoginPanel.DeactivateLoadingCircle();
        

        switch ((int)response.StatusCode)
        {
            case 200:
                user.LoggedInToken = await response.ReadContentAsStringAsync();
                ActivePlayer.LoggedInUser = user;
                LoginPanel.ToggleLoginLogoutButtons();
                LoginPanel.SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}\n Welcome {ActivePlayer.LoggedInUser.Username}.");
                break;
            case 400:
                LoginPanel.SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
                break;
            case 500:
                LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
                break;
        }
    }

    public async void Logout()
    {
        if (!ActivePlayer.IsLoggedIn()) return;

        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();

        IHttpResponseMessage response = await UserNetworkManager.LogoutAsync(ActivePlayer.LoggedInUser);

        LoginPanel.EnableLoginLogoutButtons();
        LoginPanel.DeactivateLoadingCircle();

        switch ((int)response.StatusCode)
        {
            case 200:
                ActivePlayer.Logout();
                LoginPanel.ToggleLoginLogoutButtons();
                LoginPanel.SetStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
                break;
            case 400:
                LoginPanel.SetStatus(Strings.LOGOUT_FAIL_MESSAGE);
                break;
            case 500:
                LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
                break;
        }
    }

    private bool ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool ValidatePassword(string password)
    {
        bool isPasswordLongEnough = password.Length > 7;

        return isPasswordLongEnough;
    }

    private bool ValidateUsername(string username)
    {
        Regex regex = new Regex("^[a-zA-Z0-9]{3,16}$");
        bool isUserNameValid = regex.IsMatch(username);

        return isUserNameValid;
    }
}

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public sealed class OnlineMenuController : IOnlineMenuController
{
    public IRegistrationPanel RegistrationPanel { private get; set; }
    public ILoginPanel LoginPanel { private get; set; }
    public IUserNetworkManager UserNetworkManager { private get; set; }
    public IActivePlayer ActivePlayer { private get; set; }
    public IValidator Validator { private get; set; }
    

    public async void Register(string email, string password, string username)
    {
        if (!Validator.ValidateEmail(email))
        {
            RegistrationPanel.SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!Validator.ValidatePassword(password))
        {
            RegistrationPanel.SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
            return;
        }

        if (!Validator.ValidateUsername(username))
        {
            RegistrationPanel.SetStatus(Strings.INVALID_USERNAME_MESSAGE);
            return;
        }

        RegistrationPanel.DisableRegisterButton();
        RegistrationPanel.ActivateLoadingCircle();
        RegistrationPanel.ClearStatus();

        IHttpResponseMessage response = await UserNetworkManager.CreateUserAsync(new UserDto(email, Hash(password), username));

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

        UserDto user = new UserDto(email, Hash(password));

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

    private string Hash(string input)
    {
        var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
        return string.Concat(hash.Select(b => b.ToString("x2")));
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
}

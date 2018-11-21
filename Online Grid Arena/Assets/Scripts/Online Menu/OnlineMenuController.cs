using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;

public class OnlineMenuController : IOnlineMenuController
{
    public IRegistrationPanel RegistrationPanel { protected get; set; }
    public ILoginPanel LoginPanel { protected get; set; }

    public IUserNetworkManager UserNetworkManager { protected get; set; }
    public IUserController UserController { protected get; set; }

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

        HttpResponseMessage response = await UserNetworkManager.CreateUserAsync(new UserDTO(email, Hash128.Compute(password).ToString(), username));

        RegistrationPanel.EnableRegisterButton();
        RegistrationPanel.DeactivateLoadingCircle();

        switch ((int)response.StatusCode)
        {
            case 200:
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
        if (UserController.IsLoggedIn()) return;

        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();

        HttpResponseMessage response = await UserNetworkManager.LoginAsync(new UserDTO(email, Hash128.Compute(password).ToString()));

        LoginPanel.EnableLoginLogoutButtons();
        LoginPanel.DeactivateLoadingCircle();

        switch ((int)response.StatusCode)
        {
            case 200:
                string content = await response.Content.ReadAsStringAsync();
                Debug.Log(content);
                //UserDTO responseUser = JsonConvert.DeserializeObject<UserDTO>(content);
                //UserController.LoggedInUser = responseUser;
                UserController.LoggedInUser = new UserDTO("33@33.com");
                LoginPanel.ToggleLoginLogoutButtons();
                LoginPanel.SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}");
                //LoginPanel.SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}\n Welcome {responseUser.Username}.");
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
        if (!UserController.IsLoggedIn()) return;

        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();

        HttpResponseMessage response = await UserNetworkManager.LogoutAsync(new UserDTO(UserController.Email));

        LoginPanel.EnableLoginLogoutButtons();
        LoginPanel.DeactivateLoadingCircle();

        switch ((int)response.StatusCode)
        {
            case 200:
                UserController.LoggedInUser = null;
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

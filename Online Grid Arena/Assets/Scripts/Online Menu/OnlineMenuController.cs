using System.Net.Mail;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class OnlineMenuController : IOnlineMenuController
{
    public IRegistrationPanel RegistrationPanel { protected get; set; }
    public ILoginPanel LoginPanel { protected get; set; }

    public IUserWebRequestService UserWebRequestService { protected get; set; }
    public IUserController UserController { protected get; set; }

    public void Register(string email, string password, string username)
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

        UserWebRequestService.Register(email, password, username, delegate (IWebResponse response)
        {
            RegisterCallback(response);
        });
    }

    public void RegisterCallback(IWebResponse response)
    {
        RegistrationPanel.EnableRegisterButton();
        RegistrationPanel.DeactivateLoadingCircle();

        if (response.IsNetworkError)
        {
            RegistrationPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
            return;
        }

        switch (response.ResponseCode)
        {
            case 200:
                UserDTO responseUser = JsonConvert.DeserializeObject<UserDTO>(response.ResponseText);
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

    public void Login(string email, string password)
    {
        if (UserController.IsLoggedIn()) return;

        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();

        UserWebRequestService.Login(email, password, delegate (IWebResponse response) 
        {
            LoginCallback(response);
        });
    }

    public void LoginCallback(IWebResponse response)
    {
        LoginPanel.EnableLoginLogoutButtons();
        LoginPanel.DeactivateLoadingCircle();

        if (response.IsNetworkError)
        {
            LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
            return;
        }

        switch (response.ResponseCode)
        {
            case 200:
                UserDTO responseUser = JsonConvert.DeserializeObject<UserDTO>(response.ResponseText);
                UserController.LoggedInUser = responseUser;
                LoginPanel.ToggleLoginLogoutButtons();
                LoginPanel.SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE}\n Welcome {responseUser.Username}.");
                break;
            case 400:
                LoginPanel.SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
                break;
            case 500:
                LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
                break;
        }
    }

    public void Logout()
    {
        if (!UserController.IsLoggedIn()) return;

        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();

        UserWebRequestService.Logout(UserController.Email, delegate (IWebResponse response)
        {
            LogoutCallback(response);
        });
    }

    public void LogoutCallback(IWebResponse response)
    {
        LoginPanel.EnableLoginLogoutButtons();
        LoginPanel.DeactivateLoadingCircle();

        if (response.IsNetworkError)
        {
            LoginPanel.SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
            return;
        }

        switch (response.ResponseCode)
        {
            case 200:
                UserDTO responseUser = JsonConvert.DeserializeObject<UserDTO>(response.ResponseText);
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
        return password.Length > 8;
    }

    private bool ValidateUsername(string username)
    {
        Regex regex = new Regex("[a-zA-Z0-9]{3,24}");
        return regex.IsMatch(username);
    }
}

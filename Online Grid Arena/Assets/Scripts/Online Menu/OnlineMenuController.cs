using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class OnlineMenuController : IOnlineMenuController
{
    public IRegistrationPanel RegistrationPanel { protected get; set; }
    public ILoginPanel LoginPanel { protected get; set; }

    public IAccountWebRequester AccountWebRequester { protected get; set; }
    public IAccountController AccountController { protected get; set; }

    public void SetLoginStatus(string status)
    {
        LoginPanel.SetStatus(status);
        LoginPanel.DeactivateLoadingCircle();
    }

    public void SetRegistrationStatus(string status)
    {
        RegistrationPanel.SetStatus(status);
        RegistrationPanel.DeactivateLoadingCircle();
    }

    public void ToggleLoginLogoutButtons()
    {
        LoginPanel.ToggleLoginLogoutButtons();
    }

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
        AccountWebRequester.Register(email, password, username);
    }

    public void Login(string email, string password)
    {
        if (AccountController.IsLoggedIn()) return;
        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();
        AccountWebRequester.Login(email, password);
    }

    public void Logout()
    {
        if (!AccountController.IsLoggedIn()) return;
        LoginPanel.DisableLoginLogoutButtons();
        LoginPanel.ActivateLoadingCircle();
        LoginPanel.ClearStatus();
        AccountWebRequester.Logout(AccountController.Email);
    }

    public void DisableLoginLogoutButtons()
    {
        LoginPanel.DisableLoginLogoutButtons();
    }

    public void EnableLoginLogoutButtons()
    {
        LoginPanel.EnableLoginLogoutButtons();
    }

    public void EnableRegisterButton()
    {
        RegistrationPanel.EnableRegisterButton();
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

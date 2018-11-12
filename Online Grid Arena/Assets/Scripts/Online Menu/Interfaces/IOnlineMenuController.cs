using System.Collections;

public interface IOnlineMenuController
{
    IOnlineMenuPanel RegistrationPanel { set; }
    ILoginPanel LoginPanel { set; }
    IAccountWebRequester AccountWebRequester { set; }

    void SetRegistrationStatus(string status);
    void SetLoginStatus(string status);
    void ToggleLoginLogoutButtons();

    void Register(string email, string password, string username);
    void Login(string email, string password);
    void Logout(string email, string password);
}

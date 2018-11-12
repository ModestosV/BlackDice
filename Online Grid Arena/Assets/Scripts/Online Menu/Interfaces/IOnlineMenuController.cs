using System.Collections;

public interface IOnlineMenuController
{
    IRegistrationPanel RegistrationPanel { set; }
    ILoginPanel LoginPanel { set; }
    IUserWebRequestService AccountWebRequester { set; }

    void SetRegistrationStatus(string status);
    void SetLoginStatus(string status);
    void ToggleLoginLogoutButtons();
    void DisableLoginLogoutButtons();
    void EnableLoginLogoutButtons();
    void EnableRegisterButton();

    void Register(string email, string password, string username);
    void Login(string email, string password);
    void Logout();
}

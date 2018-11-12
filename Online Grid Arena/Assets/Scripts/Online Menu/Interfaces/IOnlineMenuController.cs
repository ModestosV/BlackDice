public interface IOnlineMenuController
{
    IRegistrationPanel RegistrationPanel { set; }
    ILoginPanel LoginPanel { set; }

    IUserWebRequestService UserWebRequestService { set; }

    void Register(string email, string password, string username);
    void RegisterCallback(IWebResponse response);
    void Login(string email, string password);
    void LoginCallback(IWebResponse response);
    void Logout();
    void LogoutCallback(IWebResponse response);
}

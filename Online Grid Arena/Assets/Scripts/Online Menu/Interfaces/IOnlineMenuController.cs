public interface IOnlineMenuController
{
    IRegistrationPanel RegistrationPanel { set; }
    ILoginPanel LoginPanel { set; }

    IUserNetworkManager UserNetworkManager { set; }

    void Register(string email, string password, string username);
    void Login(string email, string password);
    void Logout();
}

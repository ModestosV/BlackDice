public interface IAccountWebRequester
{
    IAccountController AccountController { set; }

    void Register(string email, string password, string username);
    void Login(string email, string password);
    void Logout(string email);
}

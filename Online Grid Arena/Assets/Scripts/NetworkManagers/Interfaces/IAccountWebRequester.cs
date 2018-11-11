using System.Collections;

public interface IAccountWebRequester
{
    IAccountController AccountController { set; }

    IEnumerator Register(string email, string password, string username);
    IEnumerator Login(string email, string password);
    IEnumerator Logout(string email, string password);
}

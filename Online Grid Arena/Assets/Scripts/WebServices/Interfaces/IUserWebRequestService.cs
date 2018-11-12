using System;

public interface IUserWebRequestService
{
    void Register(string email, string password, string username, Action<IWebResponse> callback);
    void Login(string email, string password, Action<IWebResponse> callback);
    void Logout(string email, Action<IWebResponse> callback);
}

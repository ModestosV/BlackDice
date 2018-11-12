﻿
public interface IAccountController
{
    IOnlineMenuController OnlineMenuController { set; }

    void Register(AccountDTO user);
    void InvalidRegistration();
    void ErroredRegistration();
    void Login(AccountDTO user);
    void InvalidLogin();
    void ErroredLogin();
    void Logout(AccountDTO user);
    void InvalidLogout();
    void ErroredLogout();
}

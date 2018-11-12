using UnityEngine;
using System;

public class UserWebRequestService : WebRequestService, IUserWebRequestService
{
    public void Register(string email, string password, string username, Action<IWebResponse> callback)
    {
        UserDTO dto = new UserDTO(email, Hash128.Compute(password).ToString(), username);
        WWWForm body = dto.GetForm();

        Post($"{URLs.BASE_URL}{URLs.REGISTER_URL_PATH}", body, delegate ()
        {
            IWebResponse reponse = new WebResponse(www);
            callback(reponse);
        });
    }

    public void Login(string email, string password, Action<IWebResponse> callback)
    {
        UserDTO dto = new UserDTO(email, Hash128.Compute(password).ToString());
        WWWForm body = dto.GetForm();

        Post($"{URLs.BASE_URL}{URLs.LOGIN_URL_PATH}", body, delegate ()
        {
            IWebResponse reponse = new WebResponse(www);
            callback(reponse);
        });
    }

    public void Logout(string email, Action<IWebResponse> callback)
    {
        UserDTO dto = new UserDTO(email);
        WWWForm body = dto.GetForm();

        Post($"{URLs.BASE_URL}{URLs.LOGOUT_URL_PATH}", body, delegate ()
        {
            IWebResponse reponse = new WebResponse(www);
            callback(reponse);
        });
    }
}

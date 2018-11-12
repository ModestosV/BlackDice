using UnityEngine;
using Newtonsoft.Json;

public class AccountWebRequester : WebRequester, IAccountWebRequester
{
    public IAccountController AccountController { protected get; set; }

    public void Register(string email, string password, string username)
    {
        AccountDTO dto = new AccountDTO(email, Hash128.Compute(password).ToString(), username);
        WWWForm body = dto.GetForm();

        Post($"{URLs.BASE_URL}{URLs.REGISTER_URL_PATH}", body, delegate ()
        {
            if (www.isNetworkError)
            {
                AccountController.ErroredRegistration();
                return;
            }

            switch (www.responseCode)
            {
                case 200:
                    AccountDTO responseUser = JsonConvert.DeserializeObject<AccountDTO>(www.downloadHandler.text);
                    AccountController.Register(responseUser);
                    break;
                case 400:
                    AccountController.ErroredRegistration();
                    break;
                case 412:
                    AccountController.InvalidRegistration();
                    Debug.Log(www.downloadHandler.text);
                    break;
                case 500:
                    AccountController.ErroredRegistration();
                    Debug.Log(www.downloadHandler.text);
                    break;
            }
        });
    }

    public void Login(string email, string password)
    {
        AccountDTO dto = new AccountDTO(email, Hash128.Compute(password).ToString());
        WWWForm body = dto.GetForm();

        Post($"{URLs.BASE_URL}{URLs.LOGIN_URL_PATH}", body, delegate ()
        {
            if (www.isNetworkError)
            {
                AccountController.ErroredLogin();
                return;
            }

            switch (www.responseCode)
            {
                case 200:
                    AccountDTO responseUser = JsonConvert.DeserializeObject<AccountDTO>(www.downloadHandler.text);
                    AccountController.Login(responseUser);
                    break;
                case 400:
                    AccountController.InvalidLogin();
                    break;
                case 500:
                    AccountController.ErroredLogin();
                    break;
            }
        });
    }

    public void Logout(string email, string password)
    {
        AccountDTO dto = new AccountDTO(email, Hash128.Compute(password).ToString());
        WWWForm body = dto.GetForm();

        Post($"{URLs.BASE_URL}{URLs.LOGOUT_URL_PATH}", body, delegate ()
        {
            if (www.isNetworkError)
            {
                AccountController.ErroredLogout();
                return;
            }

            switch (www.responseCode)
            {
                case 200:
                    AccountDTO responseUser = JsonConvert.DeserializeObject<AccountDTO>(www.downloadHandler.text);
                    AccountController.Logout(responseUser);
                    break;
                case 400:
                    AccountController.InvalidLogout();
                    break;
                case 500:
                    AccountController.ErroredLogout();
                    break;
            }
        });
    }
}

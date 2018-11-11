using UnityEngine;
using Newtonsoft.Json;
using System.Collections;

public class AccountWebRequester : WebRequester, IAccountWebRequester
{
    public IAccountController AccountController { protected get; set; }

    public IEnumerator Register(string email, string password, string username)
    {
        AccountDTO dto = new AccountDTO(email, Hash128.Compute(password).ToString(), username);
        WWWForm body = dto.GetForm();
        
        yield return Post($"{URLs.BASE_URL}{URLs.REGISTER_URL_PATH}", body, delegate (long responseCode, string response)
        {
            switch (responseCode)
            {
                case 200:
                    AccountDTO responseUser = JsonConvert.DeserializeObject<AccountDTO>(response);
                    AccountController.Register(responseUser);
                    break;
                case 412:
                    AccountController.InvalidRegistration();
                    break;
                case 500:
                    AccountController.ErroredRegistration();
                    break;
            }
        });
    }

    public IEnumerator Login(string email, string password)
    {
        AccountDTO dto = new AccountDTO(email, Hash128.Compute(password).ToString());
        WWWForm body = dto.GetForm();

        yield return Post($"{URLs.BASE_URL}{URLs.LOGIN_URL_PATH}", body, delegate (long responseCode, string response)
        {
            switch (responseCode)
            {
                case 200:
                    AccountDTO responseUser = JsonConvert.DeserializeObject<AccountDTO>(response);
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

    public IEnumerator Logout(string email, string password)
    {
        AccountDTO dto = new AccountDTO(email, Hash128.Compute(password).ToString());
        WWWForm body = dto.GetForm();

        yield return Post($"{URLs.BASE_URL}{URLs.LOGOUT_URL_PATH}", body, delegate (long responseCode, string response)
        {
            switch (responseCode)
            {
                case 200:
                    AccountDTO responseUser = JsonConvert.DeserializeObject<AccountDTO>(response);
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

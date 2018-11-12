﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Net.Mail;
using System.Text.RegularExpressions;

/* 
 * A bug with the GUIs we are using causes user input length to show up as 1 size higher than intended,
 * affecting our password and username length. Our intent is for username lengths to be >=3 and passwords
 * to be >=8. Our current implementation skips the "=" in order to account for this. 
*/
public class RegistrationPanel : MonoBehaviour, IOnlineMenuPanel
{
    public Button registerButton;

    public GameObject loadingCircle;

    public LoginPanel LoginPanel;

    private UserNetworkManager UserNetworkManager { get; set; }
    private TextMeshProUGUI StatusGUI { get; set; }
    private TextMeshProUGUI EmailGUI { get; set; }
    private TextMeshProUGUI PasswordGUI { get; set; }
    private TextMeshProUGUI UsernameGUI { get; set; }

    private void OnValidate()
    {
        StatusGUI = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailGUI = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordGUI = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameGUI = GetComponentsInChildren<TextMeshProUGUI>()[4];
    }

    void Awake()
    {
        StatusGUI = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailGUI = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordGUI = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameGUI = GetComponentsInChildren<TextMeshProUGUI>()[6];
        UserNetworkManager = new UserNetworkManager();
    }

    public void Register()
    {
        LoginPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        if (!ValidateEmail(EmailGUI.text))
        {
            SetStatusText(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(PasswordGUI.text))
        {
            SetStatusText(Strings.INVALID_PASSWORD_MESSAGE);
            return;
        }

        if (!ValidateUsername(UsernameGUI.text))
        {
            SetStatusText(Strings.INVALID_USERNAME_MESSAGE);
            return;
        }

        loadingCircle.SetActive(true);
        MakeRegistrationWebRequest(EmailGUI.text, Hash128.Compute(PasswordGUI.text).ToString(), UsernameGUI.text);
        loadingCircle.SetActive(false);
    }

    public void SetStatusText(string statusText)
    {
        StatusGUI.text = statusText;
    }

    public void ClearStatus()
    {
        StatusGUI.text = "";
    }

    public void UpdateStatusText(string statusCode)
    {
        if (statusCode == "200")
        {
            SetStatusText(Strings.REGISTRATION_SUCCESS_MESSAGE);
        }
        if (statusCode == "400")
        {
            SetStatusText(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }
        if (statusCode == "412")
        {
            SetStatusText(Strings.INVALID_REQUEST_DUPLICATE_KEYS);
        }
        if (statusCode == "500")
        {
            SetStatusText(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        }
    }

    private bool ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            StatusGUI.text = Strings.INVALID_EMAIL_MESSAGE;
            return false;
        }
    }

    private bool ValidatePassword(string password)
    {
        bool isPasswordLongEnough = password.Length > 8;
        if (!isPasswordLongEnough)
        {
            StatusGUI.text = Strings.INVALID_PASSWORD_MESSAGE;
        }
        return isPasswordLongEnough;
    }

    private bool ValidateUsername(string username)
    {
        Regex regex = new Regex("[a-zA-Z0-9]{4,24}");
        bool isUserNameValid = regex.IsMatch(username);
        if(!isUserNameValid)
        {
            StatusGUI.text = Strings.INVALID_USERNAME_MESSAGE;
        }
        return isUserNameValid;
    }

    private void MakeRegistrationWebRequest(string email, string password, string username)
    {
        ClearStatus();
        UserNetworkManager.Panel = this;
        StartCoroutine(UserNetworkManager.CreateUser(new UserDTO(email, password, username)));
        loadingCircle.SetActive(false);
    }

    private IEnumerator FlickerStatus()
    {
        StatusGUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StatusGUI.gameObject.SetActive(true);
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
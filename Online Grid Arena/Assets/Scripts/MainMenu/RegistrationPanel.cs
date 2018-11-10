using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RegistrationPanel : MonoBehaviour, IPanel
{
    public TextMeshProUGUI StatusGUI { get; set; }
    public TextMeshProUGUI EmailGUI { get; set; }
    public TextMeshProUGUI PasswordGUI { get; set; }
    public TextMeshProUGUI UsernameGUI { get; set; }

    public Button registerButton;

    public GameObject loadingCircle;

    public LoginPanel LoginPanel;

    private UserNetworkManager UserNetworkManager { get; set; }

    private void OnValidate()
    {
        StatusGUI = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailGUI = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordGUI = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameGUI = GetComponentsInChildren<TextMeshProUGUI>()[4];
    }

    private void Awake()
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
            SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(PasswordGUI.text))
        {
            SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
            return;
        }

        if (!ValidateUsername(UsernameGUI.text))
        {
            SetStatus(Strings.INVALID_USERNAME_MESSAGE);
            return;
        }

        loadingCircle.SetActive(true);
        MakeRegistrationWebRequest(EmailGUI.text, Hash128.Compute(PasswordGUI.text).ToString(), UsernameGUI.text);
        loadingCircle.SetActive(false);
    }

    public void SetStatus(string statusText)
    {
        StatusGUI.text = statusText;
    }

    public void ClearStatus()
    {
        StatusGUI.text = "";
    }

    private bool ValidateEmail(string email)
    {
        bool isEmailValid = email.Contains("@");
        if (!isEmailValid)
        {
            StatusGUI.text = Strings.INVALID_EMAIL_MESSAGE;
        }
        return isEmailValid;
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
        bool isUsernameLongEnough = username.Length > 3;
        if(!isUsernameLongEnough)
        {
            StatusGUI.text = Strings.INVALID_USERNAME_MESSAGE;
        }
        return isUsernameLongEnough;
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

    public void GetStatus(string statusCode)
    {
        if (statusCode == "200")
        {
            SetStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
        }
        if (statusCode == "400")
        {
            SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }
        if (statusCode == "500")
        {
            SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        }
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
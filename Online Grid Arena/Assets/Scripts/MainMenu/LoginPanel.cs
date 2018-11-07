using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Security.Cryptography;

public class LoginPanel : MonoBehaviour
{
    public TextMeshProUGUI StatusText { get; set; }
    public TextMeshProUGUI EmailText { get; set; }
    public TextMeshProUGUI PasswordText { get; set; }

    public Button loginButton;
    public Button logoutButton;

    public GameObject loadingCircle;

    public RegistrationPanel registrationPanel;

    public string LoggedInEmail { get; set; }

    void OnValidate()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
    }

    void Awake()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
    }

    public void Login()
    {
        registrationPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        if (!ValidateEmail(EmailText.text))
        {
            SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        loadingCircle.SetActive(true);
        StartCoroutine(MakeLoginWebRequest(EmailText.text, Hash128.Compute(PasswordText.text).ToString()));
    }
    
    public void Logout()
    {
        StartCoroutine(MakeLogoutWebRequest(LoggedInEmail));
    }

    public void SetStatus(string statusText)
    {
        StatusText.text = statusText;
    }

    public void ClearStatusText()
    {
        StatusText.text = "";
    }

    public void ClearStatus()
    {
        StatusText.text = "";
    }

    public void ClearEmail()
    {
        EmailText.text = "";
    }

    public void ClearPassword()
    {
        PasswordText.text = "";
    }

    public void ToggleLoginLogoutButtons()
    {
        loginButton.gameObject.SetActive(!loginButton.gameObject.activeSelf);
        logoutButton.gameObject.SetActive(!logoutButton.gameObject.activeSelf);
    }

    private bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }

    private bool ValidatePassword(string password)
    {
        return password.Length > 1;
    }

    private IEnumerator MakeLoginWebRequest(string email, string password)
    {
        ClearStatus();
        string route = "http://localhost:5500/login";
        string parameters = $"?email={WWW.EscapeURL(email)}&password={WWW.EscapeURL(password)}";

        using (UnityWebRequest www = UnityWebRequest.Get($"{route}{parameters}"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                var response = www.downloadHandler.text;
                Debug.Log($"Login response: {response}");
                if (response != "false")
                {
                    LoggedInEmail = response;
                    SetStatus($"{Strings.LOGIN_SUCCESS_MESSAGE} \n Welcome {email}");
                    ClearEmail();
                    ClearPassword();
                    ToggleLoginLogoutButtons();
                } else
                {
                    SetStatus($"{Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE}");
                }
            }
        }
        loadingCircle.SetActive(false);
    }

    private IEnumerator MakeLogoutWebRequest(string email)
    {
        ClearStatus();
        string route = "http://localhost:5500/logout";
        string parameters = $"?email={WWW.EscapeURL(email)}";

        using (UnityWebRequest www = UnityWebRequest.Get($"{route}{parameters}"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                var response = www.downloadHandler.text;
                Debug.Log($"Logout response: {response}");
                if (response != "false")
                {
                    LoggedInEmail = null;
                    SetStatus($"{Strings.LOGOUT_SUCCESS_MESSAGE}");
                    ClearEmail();
                    ClearPassword();
                    ToggleLoginLogoutButtons();
                } else
                {
                    SetStatus($"{Strings.LOGOUT_FAIL_MESSAGE}");
                }

            }
        }
        loadingCircle.SetActive(false);
    }

    private IEnumerator FlickerStatus()
    {
        StatusText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StatusText.gameObject.SetActive(true);
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}

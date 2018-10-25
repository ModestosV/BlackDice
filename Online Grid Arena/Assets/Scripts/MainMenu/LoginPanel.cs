using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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

    private const string INVALID_EMAIL_MESSAGE = "You have not entered a valid email. Come on, you know better.";
    private const string INVALID_PASSWORD_MESSAGE = "You didn't even try to put your password in, did you?";
    private const string LOGIN_SUCCESS_MESSAGE = "You have been logged in successfully. I am so impressed.";
    private const string LOGOUT_SUCCESS_MESSAGE = "You have been logged out. Good riddance.";
    private const string INCORRECT_LOGIN_CREDENTIALS_MESSAGE = "The email and password combination you have entered is ridiculously incorrect. Try again... or don't.";
    private const string CONNECTIVITY_ISSUES_MESSAGE = "Good job chief, you broke the internet. How am I supposed to reach the login server now?";

    private void OnValidate()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
    }

    private void Awake()
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
            SetStatus(INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(PasswordText.text))
        {
            SetStatus(INVALID_PASSWORD_MESSAGE);
            return;
        }

        loadingCircle.SetActive(true);
        StartCoroutine(MakeLoginWebRequest(EmailText.text, PasswordText.text));
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
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:5500"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                SetStatus($"{LOGIN_SUCCESS_MESSAGE} \n {www.downloadHandler.text}");
                LoggedInEmail = email;
                ClearEmail();
                ClearPassword();
                ToggleLoginLogoutButtons();
            }
        }
        loadingCircle.SetActive(false);
    }

    private IEnumerator MakeLogoutWebRequest(string email)
    {
        ClearStatus();
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:5500"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                SetStatus($"{LOGOUT_SUCCESS_MESSAGE} \n {www.downloadHandler.text}");
                ClearEmail();
                ClearPassword();
                ToggleLoginLogoutButtons();
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

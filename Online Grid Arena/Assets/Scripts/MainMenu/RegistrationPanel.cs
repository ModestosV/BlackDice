using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RegistrationPanel : MonoBehaviour
{
    public LoginMenu loginMenu;

    public TextMeshProUGUI StatusText { get; set; }
    public TextMeshProUGUI EmailText { get; set; }
    public TextMeshProUGUI PasswordText { get; set; }

    public Button registerButton;

    public GameObject loadingCircle;

    public LoginPanel LoginPanel;

    private const string INVALID_EMAIL_MESSAGE = "You have not entered a valid email. Come on, you know better.";
    private const string INVALID_PASSWORD_MESSAGE = "Your password sucks! Don't you know your password has to conform to the 32 arbitrary constraints we impose on passwords?";
    private const string REGISTRATION_SUCCESS_MESSAGE = "Well, you have successfully registered with those credentials, but you could probably do a lot better.";
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

    public void Register()
    {
        LoginPanel.ClearStatus();
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
        StartCoroutine(MakeRegistrationWebRequest(EmailText.text, PasswordText.text));
    }

    public void SetStatus(string statusText)
    {
        StatusText.text = statusText;
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

    private bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }

    private bool ValidatePassword(string password)
    {
        return password.Length > 8;
    }

    private IEnumerator MakeRegistrationWebRequest(string email, string password)
    {
        ClearStatus();
        string route = "http://localhost:5500/register";
        string parameters = $"?email={WWW.EscapeURL(email)}&password={WWW.EscapeURL(password)}";

        using (UnityWebRequest www = UnityWebRequest.Get($"{route}{parameters}"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                SetStatus($"{REGISTRATION_SUCCESS_MESSAGE} \n {www.downloadHandler.text}");
                ClearEmail();
                ClearPassword();
            }
            loadingCircle.SetActive(false);
        }
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

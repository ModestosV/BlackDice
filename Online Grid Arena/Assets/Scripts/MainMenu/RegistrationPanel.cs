using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RegistrationPanel : MonoBehaviour
{
    public TextMeshProUGUI StatusText { get; set; }
    public TextMeshProUGUI EmailText { get; set; }
    public TextMeshProUGUI PasswordText { get; set; }

    public Button registerButton;

    public GameObject loadingCircle;

    public LoginPanel LoginPanel;

    private const string INVALID_EMAIL_MESSAGE = "You have not entered a valid email.";
    private const string INVALID_PASSWORD_MESSAGE = "Your password must be at least 8 characters long.";
    private const string REGISTRATION_SUCCESS_MESSAGE = "You have successfully registered this account!";
    private const string CONNECTIVITY_ISSUES_MESSAGE = "Error: Web connectivity issues.";

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
        StartCoroutine(MakeRegistrationWebRequest(EmailText.text, Hash128.Compute(PasswordText.text).ToString()));
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
        return password.Length > 8; // There is an invisible unicode charater in the text fields of TextMesh Pro objects that I can't seem to get rid of.
    }

    private IEnumerator MakeRegistrationWebRequest(string email, string password)
    {
        ClearStatus();
        string route = "http://localhost:5500/register";
        using (UnityWebRequest www = UnityWebRequest.Post(route + "?" + email + "&password=" + password, "POST method for registration"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                SetStatus($"{REGISTRATION_SUCCESS_MESSAGE}");
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

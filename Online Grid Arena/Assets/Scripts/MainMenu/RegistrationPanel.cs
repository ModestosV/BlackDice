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

    public void Register()
    {
        LoginPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        if (!ValidateEmail(EmailText.text))
        {
            SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(PasswordText.text))
        {
            SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
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
        string parameters = $"?email={WWW.EscapeURL(email)}&password={WWW.EscapeURL(password)}";

        using (UnityWebRequest www = UnityWebRequest.Get($"{route}{parameters}"))
        {
            yield return www.SendWebRequest();

            var response = www.downloadHandler.text;
            Debug.Log($"Logout response: {response}");

            if (www.isNetworkError || www.isHttpError)
            {
                SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
            }
            else
            {
                SetStatus($"{Strings.REGISTRATION_SUCCESS_MESSAGE}");
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

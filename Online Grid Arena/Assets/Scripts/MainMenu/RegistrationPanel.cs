using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RegistrationPanel : MonoBehaviour, IPanel
{
    public TextMeshProUGUI StatusText { get; set; }
    public TextMeshProUGUI EmailText { get; set; }
    public TextMeshProUGUI PasswordText { get; set; }
    public TextMeshProUGUI UsernameText { get; set; }

    public Button registerButton;

    public GameObject loadingCircle;

    public LoginPanel LoginPanel;

    private void OnValidate()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameText = GetComponentsInChildren<TextMeshProUGUI>()[4];
    }

    private void Awake()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameText = GetComponentsInChildren<TextMeshProUGUI>()[6];
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
        MakeRegistrationWebRequest(EmailText.text, Hash128.Compute(PasswordText.text).ToString(), UsernameText.text);
        loadingCircle.SetActive(false);
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

    public void ClearUsername()
    {
        UsernameText.text = "";
    }

    private bool ValidateEmail(string email)
    {
        return true; // TODO: Add validation
    }

    private bool ValidatePassword(string password)
    {
        return true; // TODO: Add validation
    }

    private bool ValidateUsername(string username)
    {
        return true; // TODO: Add validation 
    }

    private void MakeRegistrationWebRequest(string email, string password, string username)
    {
        ClearStatus();
        UserNetworkManager unm = new UserNetworkManager();
        unm.Panel = this;
        StartCoroutine(unm.CreateUser(new UserDto(email, password, username)));
        loadingCircle.SetActive(false);
    }

    private IEnumerator FlickerStatus()
    {
        StatusText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StatusText.gameObject.SetActive(true);
    }

    public void GetStatus(AbstractNetworkManager anm)
    {
        if (anm.StatusCode == "200")
        {
            SetStatus(Strings.REGISTRATION_SUCCESS_MESSAGE);
        }
        if (anm.StatusCode == "400")
        {
            SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }
        if (anm.StatusCode == "500")
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

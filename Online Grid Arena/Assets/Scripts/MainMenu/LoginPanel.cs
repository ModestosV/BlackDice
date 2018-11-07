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

        loadingCircle.SetActive(true);
        MakeLoginWebRequest(EmailText.text, Hash128.Compute(PasswordText.text).ToString());
    }

    public void Logout()
    {
        registrationPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        loadingCircle.SetActive(true);
        MakeLogoutWebRequest(EmailText.text, Hash128.Compute(PasswordText.text).ToString());
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

    private void MakeLoginWebRequest(string email, string password)
    {
        ClearStatus();
        UserNetworkManager unm = new UserNetworkManager();
        StartCoroutine(unm.Login(new UserDto(email, password)));
        if (unm.StatusCode == "200")
        {
            SetStatus(Strings.LOGIN_SUCCESS_MESSAGE);
            ToggleLoginLogoutButtons();
        }
        if (unm.StatusCode == "400")
        {
            SetStatus(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }

        if (unm.StatusCode == "500")
        {
            SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        }
        loadingCircle.SetActive(false);
    }

    private void MakeLogoutWebRequest(string email, string password)
    {
        ClearStatus();
        UserNetworkManager unm = new UserNetworkManager();
        StartCoroutine(unm.Logout(new UserDto(email, password)));
        if (unm.StatusCode == "200")
        {
            SetStatus(Strings.LOGOUT_SUCCESS_MESSAGE);
            ToggleLoginLogoutButtons();
        }
        if (unm.StatusCode == "400")
        {
            SetStatus(Strings.LOGOUT_FAIL_MESSAGE);
        }

        if (unm.StatusCode == "500")
        {
            SetStatus(Strings.CONNECTIVITY_ISSUES_MESSAGE);
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

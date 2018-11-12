using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoginPanel : MonoBehaviour, IOnlineMenuPanel
{
    public Button loginButton;
    public Button logoutButton;

    public GameObject loadingCircle;

    public RegistrationPanel registrationPanel;

    public string LoggedInEmail { get; set; }

    private TextMeshProUGUI StatusGUI { get; set; }
    private TextMeshProUGUI EmailGUI { get; set; }
    private TextMeshProUGUI PasswordGUI { get; set; }
    private UserNetworkManager UserNetworkManager { get; set; }

    private void OnValidate()
    {
        StatusGUI = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailGUI = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordGUI = GetComponentsInChildren<TextMeshProUGUI>()[3];
    }

    void Awake()
    {
        StatusGUI = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailGUI = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordGUI = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UserNetworkManager = new UserNetworkManager();
    }

    public void Login()
    {
        registrationPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        loadingCircle.SetActive(true);
        MakeLoginWebRequest(EmailGUI.text, Hash128.Compute(PasswordGUI.text).ToString());
    }

    public void Logout()
    {
        registrationPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        loadingCircle.SetActive(true);
        MakeLogoutWebRequest(EmailGUI.text, Hash128.Compute(PasswordGUI.text).ToString());
    }

    public void SetStatusText(string statusText)
    {
        StatusGUI.text = statusText;
    }

    public void ClearStatus()
    {
        StatusGUI.text = "";
    }

    public void ClearEmail()
    {
        EmailGUI.text = "";
    }

    public void ClearPassword()
    {
        PasswordGUI.text = "";
    }

    public void ToggleLoginLogoutButtons()
    {
        loginButton.gameObject.SetActive(!loginButton.gameObject.activeSelf);
        logoutButton.gameObject.SetActive(!logoutButton.gameObject.activeSelf);
        if(loginButton.gameObject.activeSelf)
        {
            SetStatusText(Strings.LOGOUT_SUCCESS_MESSAGE);
        }
        else
        {
            SetStatusText(Strings.LOGIN_SUCCESS_MESSAGE);
        }
    }

    private void MakeLoginWebRequest(string email, string password)
    {
        ClearStatus();
        UserNetworkManager.Panel = this;
        StartCoroutine(UserNetworkManager.Login(new UserDTO(email, password)));
        loadingCircle.SetActive(false);
    }

    private void MakeLogoutWebRequest(string email, string password)
    {
        ClearStatus();
        UserNetworkManager.Panel = this;
        StartCoroutine(UserNetworkManager.Logout(new UserDTO(email, password)));
        loadingCircle.SetActive(false);
    }

    private IEnumerator FlickerStatus()
    {
        StatusGUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StatusGUI.gameObject.SetActive(true);
    }

    public void UpdateStatusText(string statusCode)
    {
        if (statusCode == "200")
        {
            ToggleLoginLogoutButtons();
        }
        if (statusCode == "400")
        {
            SetStatusText(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }
        if (statusCode == "500")
        {
            SetStatusText(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        }
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}

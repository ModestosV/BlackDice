using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Threading.Tasks;
using System.Net.Http;

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

    void Awake()
    {
        StatusGUI = GameObject.Find("Status").GetComponentInChildren<TextMeshProUGUI>();
        EmailGUI = GameObject.Find("EmailField").GetComponentInChildren<TextMeshProUGUI>();
        PasswordGUI = GameObject.Find("PasswordField").GetComponentInChildren<TextMeshProUGUI>();
        UserNetworkManager = new UserNetworkManager();
    }

    public async void LoginAsync()
    {
        registrationPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        loadingCircle.SetActive(true);
        await MakeLoginWebRequestAsync(EmailGUI.text, Hash128.Compute(PasswordGUI.text).ToString());
    }

    public async void LogoutAsync()
    {
        registrationPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        loadingCircle.SetActive(true);
        await MakeLogoutWebRequestAsync(EmailGUI.text, Hash128.Compute(PasswordGUI.text).ToString());
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

    private async Task MakeLoginWebRequestAsync(string email, string password)
    {
        ClearStatus();
        UserNetworkManager.Panel = this;
        HttpResponseMessage response = await UserNetworkManager.LoginAsync(new UserDTO(email, password));
        UpdateStatusText((int)response.StatusCode);
        loadingCircle.SetActive(false);
    }

    private async Task MakeLogoutWebRequestAsync(string email, string password)
    {
        ClearStatus();
        UserNetworkManager.Panel = this;
        HttpResponseMessage response = await UserNetworkManager.LogoutAsync(new UserDTO(email, password));
        UpdateStatusText((int)response.StatusCode);
        loadingCircle.SetActive(false);
    }

    private IEnumerator FlickerStatus()
    {
        StatusGUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StatusGUI.gameObject.SetActive(true);
    }

    public void UpdateStatusText(int statusCode)
    {
        if (statusCode == 200)
        {
            ToggleLoginLogoutButtons();
        }
        if (statusCode == 400)
        {
            SetStatusText(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }
        if (statusCode == 500)
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

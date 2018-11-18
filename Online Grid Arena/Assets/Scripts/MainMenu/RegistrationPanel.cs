using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

public class RegistrationPanel : MonoBehaviour, IOnlineMenuPanel
{
    public Button registerButton;

    public GameObject loadingCircle;

    public LoginPanel LoginPanel;

    private UserNetworkManager UserNetworkManager { get; set; }
    private TextMeshProUGUI StatusGUI { get; set; }
    private TMP_InputField EmailInputField { get; set; }
    private TMP_InputField PasswordInputField { get; set; }
    private TMP_InputField UsernameInputField { get; set; }

    void Awake()
    {
        StatusGUI = gameObject.transform.Find("Status").GetComponentInChildren<TextMeshProUGUI>();
        EmailInputField = gameObject.transform.Find("EmailField").GetComponentInChildren<TMP_InputField>();
        PasswordInputField = gameObject.transform.Find("PasswordField").GetComponentInChildren<TMP_InputField>();
        UsernameInputField = gameObject.transform.Find("UsernameField").GetComponentInChildren<TMP_InputField>();
        UserNetworkManager = new UserNetworkManager();
    }

    public async void RegisterAsync()
    {
        LoginPanel.ClearStatus();
        StartCoroutine(FlickerStatus());

        if (!ValidateEmail(EmailInputField.text))
        {
            SetStatusText(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(PasswordInputField.text))
        {
            SetStatusText(Strings.INVALID_PASSWORD_MESSAGE);
            return;
        }

        if (!ValidateUsername(UsernameInputField.text))
        {
            SetStatusText(Strings.INVALID_USERNAME_MESSAGE);
            return;
        }

        loadingCircle.SetActive(true);
        await MakeRegistrationWebRequestAsync(EmailInputField.text, Hash128.Compute(PasswordInputField.text).ToString(), UsernameInputField.text);
    }

    public void SetStatusText(string statusText)
    {
        StatusGUI.text = statusText;
    }

    public void ClearStatus()
    {
        StatusGUI.text = "";
    }

    public void UpdateStatusText(int statusCode)
    {
        if (statusCode == 200)
        {
            SetStatusText(Strings.REGISTRATION_SUCCESS_MESSAGE);
        }
        if (statusCode == 400)
        {
            SetStatusText(Strings.INVALID_LOGIN_CREDENTIALS_MESSAGE);
        }
        if (statusCode == 412)
        {
            SetStatusText(Strings.INVALID_REQUEST_DUPLICATE_KEYS);
        }
        if (statusCode == 500)
        {
            SetStatusText(Strings.CONNECTIVITY_ISSUES_MESSAGE);
        }
    }

    private bool ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            StatusGUI.text = Strings.INVALID_EMAIL_MESSAGE;
            return false;
        }
    }

    private bool ValidatePassword(string password)
    {
        bool isPasswordLongEnough = password.Length > 7;
        if (!isPasswordLongEnough)
        {
            StatusGUI.text = Strings.INVALID_PASSWORD_MESSAGE;
        }
        return isPasswordLongEnough;
    }

    private bool ValidateUsername(string username)
    {
        Regex regex = new Regex("^[a-zA-Z0-9]{3,16}$");
        bool isUserNameValid = regex.IsMatch(username);
        if (!isUserNameValid)
        {
            StatusGUI.text = Strings.INVALID_USERNAME_MESSAGE;
        }
        return isUserNameValid;
    }

    private async Task MakeRegistrationWebRequestAsync(string email, string password, string username)
    {
        ClearStatus();
        UserNetworkManager.Panel = this;
        HttpResponseMessage response = await UserNetworkManager.CreateUserAsync(new UserDTO(email, password, username));
        UpdateStatusText((int)response.StatusCode);
        loadingCircle.SetActive(false);
    }

    private IEnumerator FlickerStatus()
    {
        StatusGUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StatusGUI.gameObject.SetActive(true);
    }
}



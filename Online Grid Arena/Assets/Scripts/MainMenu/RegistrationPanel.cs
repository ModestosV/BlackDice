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
            SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (!ValidatePassword(PasswordText.text))
        {
            SetStatus(Strings.INVALID_PASSWORD_MESSAGE);
            return;
        }

        loadingCircle.SetActive(true);
        MakeRegistrationWebRequest(EmailText.text, Hash128.Compute(PasswordText.text).ToString());
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
        return true;
    }

    private bool ValidatePassword(string password)
    {
        return true; // There is an invisible unicode charater in the text fields of TextMesh Pro objects that I can't seem to get rid of.
    }

    private void MakeRegistrationWebRequest(string email, string password)
    {
        ClearStatus();
        UserNetworkManager unm = new UserNetworkManager();
        StartCoroutine(unm.CreateUser(new UserDto(email, "defaultUserName", password)));
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

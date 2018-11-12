using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoginPanel : MonoBehaviour, ILoginPanel
{
    public Button loginButton;
    public Button logoutButton;

    public GameObject loadingCircle;

    public IOnlineMenuController OnlineMenuController { protected get; set; }

    private TextMeshProUGUI StatusText { get; set; }
    private TextMeshProUGUI EmailText { get; set; }
    private TextMeshProUGUI PasswordText { get; set; }

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
        OnlineMenuController.Login(EmailText.text, PasswordText.text);
    }

    public void Logout()
    {
        OnlineMenuController.Logout(EmailText.text, PasswordText.text);
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

    public void ToggleLoginLogoutButtons()
    {
        loginButton.gameObject.SetActive(!loginButton.gameObject.activeSelf);
        logoutButton.gameObject.SetActive(!logoutButton.gameObject.activeSelf);
    }

    public void ActivateLoadingCircle()
    {
        loadingCircle.SetActive(true);
    }

    public void DeactivateLoadingCircle()
    {
        loadingCircle.SetActive(false);
    }
}

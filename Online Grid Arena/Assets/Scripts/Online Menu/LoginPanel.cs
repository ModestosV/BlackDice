using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoginPanel : Panel, ILoginPanel
{
    public Button loginButton;
    public Button logoutButton;

    public IOnlineMenuController OnlineMenuController { protected get; set; }

    private TMP_InputField EmailInputField { get; set; }
    private TMP_InputField PasswordInputField { get; set; }

    private void OnValidate()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        PasswordInputField = GetComponentsInChildren<TMP_InputField>()[1];
    }

    private void Awake()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        PasswordInputField = GetComponentsInChildren<TMP_InputField>()[1];
    }

    public void Login()
    {
        OnlineMenuController.Login(EmailInputField.text, PasswordInputField.text);
    }

    public void Logout()
    {
        OnlineMenuController.Logout();
    }

    public void ToggleLoginLogoutButtons()
    {
        loginButton.gameObject.SetActive(!loginButton.gameObject.activeSelf);
        logoutButton.gameObject.SetActive(!logoutButton.gameObject.activeSelf);
    }

    public void EnableLoginLogoutButtons()
    {
        loginButton.interactable = true;
        logoutButton.interactable = true;
    }

    public void DisableLoginLogoutButtons()
    {
        loginButton.interactable = false;
        logoutButton.interactable = false;
    }
}

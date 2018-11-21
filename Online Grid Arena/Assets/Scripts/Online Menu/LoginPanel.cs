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
        EmailInputField.text = "";
    }

    public void ClearPassword()
    {
        PasswordInputField.text = "";
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

    public void ActivateLoadingCircle()
    {
        loadingCircle.SetActive(true);
    }

    public void DeactivateLoadingCircle()
    {
        loadingCircle.SetActive(false);
    }
}

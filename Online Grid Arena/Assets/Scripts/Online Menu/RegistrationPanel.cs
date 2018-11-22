using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationPanel : Panel, IRegistrationPanel 
{
    public Button registerButton;

    public IOnlineMenuController OnlineMenuController { protected get; set; }

    private TMP_InputField EmailInputField { get; set; }
    private TMP_InputField PasswordInputField { get; set; }
    private TMP_InputField UsernameInputField { get; set; }
    private TextMeshProUGUI StatusText { get; set; }

    private void OnValidate()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        PasswordInputField = GetComponentsInChildren<TMP_InputField>()[1];
        UsernameInputField = GetComponentsInChildren<TMP_InputField>()[2];
    }

    private void Awake()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        PasswordInputField = GetComponentsInChildren<TMP_InputField>()[1];
        UsernameInputField = GetComponentsInChildren<TMP_InputField>()[2];
    }

    public void Register()
    {
        OnlineMenuController.Register(EmailInputField.text, PasswordInputField.text, UsernameInputField.text);
    }

    public void EnableRegisterButton()
    {
        registerButton.interactable = true;
    }

    public void DisableRegisterButton()
    {
        registerButton.interactable = false;
    }

    public override void SetStatus(string statusText)
    {
        StatusText.text = statusText;
    }

    public override void ClearStatus()
    {
        StatusText.text = "";
    }
}
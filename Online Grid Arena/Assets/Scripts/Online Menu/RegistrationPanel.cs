using UnityEngine.UI;
using TMPro;

public sealed class RegistrationPanel : Panel, IRegistrationPanel 
{
    private Button registerButton;

    public IOnlineMenuController OnlineMenuController { private get; set; }

    private TMP_InputField emailInputField;
    private TMP_InputField passwordInputField;
    private TMP_InputField usernameInputField;

    private void OnValidate()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        statusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        registerButton = GetComponentInChildren<Button>();
        emailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        passwordInputField = GetComponentsInChildren<TMP_InputField>()[1];
        usernameInputField = GetComponentsInChildren<TMP_InputField>()[2];
    }

    private void Awake()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        statusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        registerButton = GetComponentInChildren<Button>();
        emailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        passwordInputField = GetComponentsInChildren<TMP_InputField>()[1];
        usernameInputField = GetComponentsInChildren<TMP_InputField>()[2];
    }

    void Start()
    {
        loadingCircle.gameObject.SetActive(false);
    }

    public void Register()
    {
        OnlineMenuController.Register(emailInputField.text, passwordInputField.text, usernameInputField.text);
    }

    public void EnableRegisterButton()
    {
        registerButton.interactable = true;
    }

    public void DisableRegisterButton()
    {
        registerButton.interactable = false;
    }
}
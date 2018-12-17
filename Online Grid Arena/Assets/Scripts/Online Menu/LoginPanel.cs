using UnityEngine.UI;
using TMPro;

public sealed class LoginPanel : Panel, ILoginPanel
{
    private Button loginButton;
    private Button logoutButton;

    public IOnlineMenuController OnlineMenuController { private get; set; }

    private TMP_InputField emailInputField;
    private TMP_InputField passwordInputField;

    void OnValidate()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        loginButton = GetComponentsInChildren<Button>()[0];
        logoutButton = GetComponentsInChildren<Button>()[1];
        statusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        emailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        passwordInputField = GetComponentsInChildren<TMP_InputField>()[1];
    }

    void Awake()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        loginButton = GetComponentsInChildren<Button>()[0];
        logoutButton = GetComponentsInChildren<Button>()[1];
        statusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        emailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        passwordInputField = GetComponentsInChildren<TMP_InputField>()[1];
    }

    void Start()
    {
        logoutButton.gameObject.SetActive(false);
        loadingCircle.gameObject.SetActive(false);
    }

    public void Login()
    {
        OnlineMenuController.Login(emailInputField.text, passwordInputField.text);
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

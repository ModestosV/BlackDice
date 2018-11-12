using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistrationPanel : MonoBehaviour, IRegistrationPanel
{
    public Button registerButton;
    public GameObject loadingCircle;

    public IOnlineMenuController OnlineMenuController { protected get; set; }

    private TextMeshProUGUI StatusText { get; set; }
    private TextMeshProUGUI EmailText { get; set; }
    private TextMeshProUGUI PasswordText { get; set; }
    private TextMeshProUGUI UsernameText { get; set; }

    private void OnValidate()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameText = GetComponentsInChildren<TextMeshProUGUI>()[4];
    }

    private void Awake()
    {
        StatusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        EmailText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        PasswordText = GetComponentsInChildren<TextMeshProUGUI>()[3];
        UsernameText = GetComponentsInChildren<TextMeshProUGUI>()[6];
    }

    public void Register()
    {
        OnlineMenuController.Register(EmailText.text, PasswordText.text, UsernameText.text);
    }

    public void SetStatus(string statusText)
    {
        StatusText.text = statusText;
    }

    public void ClearStatus()
    {
        StatusText.text = "";
    }

    public void ActivateLoadingCircle()
    {
        loadingCircle.SetActive(true);
    }

    public void DeactivateLoadingCircle()
    {
        loadingCircle.SetActive(false);
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
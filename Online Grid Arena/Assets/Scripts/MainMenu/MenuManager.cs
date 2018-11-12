using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private LoginPanel loginPanel;
    private RegistrationPanel registrationPanel;
    private OnlineMenuController onlineMenuController;
    private AccountWebRequester accountWebRequester;
    private AccountController accountController;

    private void Awake()
    {
        loginPanel = FindObjectOfType<LoginPanel>();
        registrationPanel = FindObjectOfType<RegistrationPanel>();
        accountWebRequester = FindObjectOfType<AccountWebRequester>();

        onlineMenuController = new OnlineMenuController
        {
            LoginPanel = loginPanel,
            RegistrationPanel = registrationPanel,
            AccountWebRequester = accountWebRequester
        };

        loginPanel.OnlineMenuController = onlineMenuController;
        registrationPanel.OnlineMenuController = onlineMenuController;

        accountController = new AccountController()
        {
            OnlineMenuController = onlineMenuController
        };

        accountWebRequester.AccountController = accountController;
    }
}

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private LoginPanel loginPanel;
    private RegistrationPanel registrationPanel;
    private OnlineMenuController onlineMenuController;
    private UserWebRequestService accountWebRequester;
    private UserController accountController;

    private void Awake()
    {
        loginPanel = FindObjectOfType<LoginPanel>();
        registrationPanel = FindObjectOfType<RegistrationPanel>();
        accountWebRequester = FindObjectOfType<UserWebRequestService>();

        onlineMenuController = new OnlineMenuController
        {
            LoginPanel = loginPanel,
            RegistrationPanel = registrationPanel,
            AccountWebRequester = accountWebRequester
        };

        loginPanel.OnlineMenuController = onlineMenuController;
        registrationPanel.OnlineMenuController = onlineMenuController;

        accountController = new UserController()
        {
            OnlineMenuController = onlineMenuController
        };

        accountWebRequester.AccountController = accountController;
        onlineMenuController.AccountController = accountController;
    }
}

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private LoginPanel loginPanel;
    private RegistrationPanel registrationPanel;
    private OnlineMenuController onlineMenuController;
    private UserWebRequestService userWebRequestService;
    private UserController userController;

    private void Awake()
    {
        loginPanel = FindObjectOfType<LoginPanel>();
        registrationPanel = FindObjectOfType<RegistrationPanel>();
        userWebRequestService = FindObjectOfType<UserWebRequestService>();
        userController = new UserController();

        onlineMenuController = new OnlineMenuController
        {
            LoginPanel = loginPanel,
            RegistrationPanel = registrationPanel,
            UserWebRequestService = userWebRequestService,
            UserController = userController
        };

        loginPanel.OnlineMenuController = onlineMenuController;
        registrationPanel.OnlineMenuController = onlineMenuController;
    }
}

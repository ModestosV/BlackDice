using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private LoginPanel loginPanel;
    private RegistrationPanel registrationPanel;
    private OnlineMenuController onlineMenuController;
    private UserNetworkManager userNetworkManager;
    private ActivePlayer userController;

    private void Awake()
    {
        loginPanel = FindObjectOfType<LoginPanel>();
        registrationPanel = FindObjectOfType<RegistrationPanel>();
        userNetworkManager = new UserNetworkManager();
        userController = new ActivePlayer();

        onlineMenuController = new OnlineMenuController
        {
            LoginPanel = loginPanel,
            RegistrationPanel = registrationPanel,
            UserNetworkManager = userNetworkManager,
            UserController = userController
        };

        loginPanel.OnlineMenuController = onlineMenuController;
        registrationPanel.OnlineMenuController = onlineMenuController;
    }
}

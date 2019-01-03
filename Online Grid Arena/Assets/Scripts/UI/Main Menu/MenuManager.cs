using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private LoginPanel loginPanel;
    private RegistrationPanel registrationPanel;
    private FeedbackPanel feedbackPanel;
    private OnlineMenuController onlineMenuController;
    private FeedbackMenuController feedbackMenuController;

    private void Awake()
    {
        loginPanel = FindObjectOfType<LoginPanel>();
        registrationPanel = FindObjectOfType<RegistrationPanel>();
        feedbackPanel = FindObjectOfType<FeedbackPanel>();

        onlineMenuController = new OnlineMenuController
        {
            LoginPanel = loginPanel,
            RegistrationPanel = registrationPanel,
            UserNetworkManager = new UserNetworkManager(),
            ActivePlayer = new ActivePlayer(),
            Validator = new Validator()
        };

        loginPanel.OnlineMenuController = onlineMenuController;
        registrationPanel.OnlineMenuController = onlineMenuController;

        feedbackMenuController = new FeedbackMenuController(feedbackPanel, new FeedbackNetworkManager(), new Validator());
        feedbackPanel.FeedbackMenuController = feedbackMenuController;
    }
}

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private LoginPanel loginPanel;
    private RegistrationPanel registrationPanel;
    private FeedbackPanel feedbackPanel;
    private OnlineMenuController onlineMenuController;
    private FeedbackMenuController feedbackMenuController;
    private Validator validator;

    private void Awake()
    {
        new LogHandler();
        loginPanel = FindObjectOfType<LoginPanel>();
        registrationPanel = FindObjectOfType<RegistrationPanel>();
        feedbackPanel = FindObjectOfType<FeedbackPanel>();
        validator = new Validator();

        onlineMenuController = new OnlineMenuController
        {
            LoginPanel = loginPanel,
            RegistrationPanel = registrationPanel,
            UserNetworkManager = new UserNetworkManager(),
            ActivePlayer = new ActivePlayer(),
            Validator = validator
        };

        loginPanel.OnlineMenuController = onlineMenuController;
        registrationPanel.OnlineMenuController = onlineMenuController;

        feedbackMenuController = new FeedbackMenuController(feedbackPanel, new FeedbackNetworkManager(), validator);
        feedbackPanel.FeedbackMenuController = feedbackMenuController;
    }
}

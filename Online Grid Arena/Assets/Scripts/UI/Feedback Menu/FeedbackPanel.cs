using TMPro;

public sealed class FeedbackPanel : Panel, IFeedbackPanel
{
    public IFeedbackMenuController FeedbackMenuController { private get; set; }
    
    private TMP_InputField emailInputField;
    private TMP_InputField feedbackInputField;

    void Awake()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        statusText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        emailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        feedbackInputField = GetComponentsInChildren<TMP_InputField>()[1];
    }

    void Start()
    {
        loadingCircle.gameObject.SetActive(false);
    }

    public void Send()
    {
        FeedbackMenuController.SubmitFeedbackAsync(emailInputField.text, feedbackInputField.text);
    }
}

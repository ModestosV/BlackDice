using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class FeedbackPanel : Panel, IFeedbackPanel
{
    public IFeedbackMenuController FeedbackMenuController { private get; set; }

    private Button sendButton;

    private TMP_InputField emailInputField;
    private TMP_InputField feedbackInputField;

    void Awake()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        sendButton = GetComponentsInChildren<Button>()[0];
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

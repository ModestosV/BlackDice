﻿using TMPro;
using UnityEngine.UI;

public sealed class FeedbackPanel : Panel
{
    public IFeedbackMenuController FeedbackMenuController { private get; set; }

    private Button sendButton;

    private TMP_InputField emailInputField;
    private TMP_InputField feedbackInputField;

    void Awake()
    {
        loadingCircle = GetComponentInChildren<LoadingCircle>();
        sendButton = GetComponentsInChildren<Button>()[0];
        statusText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        emailInputField = GetComponentsInChildren<TMP_InputField>()[0];
        feedbackInputField = GetComponentsInChildren<TMP_InputField>()[1];
    }

    void Start()
    {
        loadingCircle.gameObject.SetActive(false);
    }

    public void Login()
    {
        FeedbackMenuController.SubmitFeedback(emailInputField.text, feedbackInputField.text);
    }
}

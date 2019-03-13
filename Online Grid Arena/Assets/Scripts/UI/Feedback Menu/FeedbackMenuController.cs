
public sealed class FeedbackMenuController : IFeedbackMenuController
{
    private IFeedbackPanel feedbackPanel;
    private IFeedbackNetworkManager feedbackNetworkManager;
    private IValidator validator;

    public FeedbackMenuController(
        IFeedbackPanel feedbackPanel,
        IFeedbackNetworkManager feedbackNetworkManager,
        IValidator validator
        )
    {
        this.feedbackPanel = feedbackPanel;
        this.feedbackNetworkManager = feedbackNetworkManager;
        this.validator = validator;
    }

    public async void SubmitFeedbackAsync(string email, string feedback)
    {
        if (!validator.ValidateEmail(email))
        {
            feedbackPanel.SetStatus(Strings.INVALID_EMAIL_MESSAGE);
            return;
        }

        if (feedback.Length < 10)
        {
            feedbackPanel.SetStatus(Strings.SEND_FEEDBACK_SHORT_MESSAGE);
            return;
        }

        feedbackPanel.ClearStatus();
        feedbackPanel.ActivateLoadingCircle();

        IHttpResponseMessage response = await feedbackNetworkManager.SendFeedbackAsync(new FeedbackDto(email, feedback));

        feedbackPanel.DeactivateLoadingCircle();

        switch ((int)response.StatusCode)
        {
            case 200:
                feedbackPanel.SetStatus(Strings.SEND_FEEDBACK_SUCCESS_MESSAGE);
                feedbackPanel.ClearFields();
                break;
            case 500:
                feedbackPanel.SetStatus(Strings.SERVER_ERROR_MESSAGE);
                break;
        }
    }
}


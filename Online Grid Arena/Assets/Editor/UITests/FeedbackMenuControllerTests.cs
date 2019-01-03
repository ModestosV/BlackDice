using NSubstitute;
using NUnit.Framework;
using System.Net;

public class FeedbackMenuControllerTests
{
    FeedbackMenuController sut;
    IFeedbackPanel feedbackPanel;
    IFeedbackNetworkManager feedbackNetworkManager;
    IHttpResponseMessage responseMessage;
    
    [SetUp]
    public void Init()
    {
        feedbackPanel = Substitute.For<IFeedbackPanel>();
        feedbackNetworkManager = Substitute.For<IFeedbackNetworkManager>();
        responseMessage = Substitute.For<IHttpResponseMessage>();

        sut = new FeedbackMenuController(feedbackPanel, feedbackNetworkManager, new Validator());
    }

    [Test]
    public void Send_feedback_with_valid_email()
    {
        string validEmail = "foo@bar.com";
        string message = "Message goes here";
        responseMessage.StatusCode = HttpStatusCode.OK;

        feedbackNetworkManager.SendFeedbackAsync(Arg.Is<FeedbackDto>(x => x.Email == validEmail && x.Message == message)).Returns(responseMessage);

        sut.SubmitFeedbackAsync(validEmail, message);

        feedbackPanel.Received(1).ActivateLoadingCircle();
        feedbackNetworkManager.Received(1).SendFeedbackAsync(Arg.Is<FeedbackDto>((x => x.Email == validEmail && x.Message == message)));
        feedbackPanel.Received(1).DeactivateLoadingCircle();
        feedbackPanel.Received(1).SetStatus(Strings.SEND_FEEDBACK_SUCCESS_MESSAGE);
    }

    [Test]
    public void Send_feedback_with_invalid_email()
    {
        string validEmail = "foo";
        string message = "Message goes here";
        feedbackNetworkManager.SendFeedbackAsync(Arg.Is<FeedbackDto>(x => x.Email == validEmail && x.Message == message)).Returns(responseMessage);

        sut.SubmitFeedbackAsync(validEmail, message);

        feedbackPanel.Received(0).ActivateLoadingCircle();
        feedbackNetworkManager.Received(0).SendFeedbackAsync(Arg.Any<FeedbackDto>());
        feedbackPanel.Received(0).DeactivateLoadingCircle();
        feedbackPanel.Received(1).SetStatus(Strings.INVALID_EMAIL_MESSAGE);
    }
}
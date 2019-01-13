using System.Threading.Tasks;

public interface IFeedbackNetworkManager
{
    Task<IHttpResponseMessage> SendFeedbackAsync(FeedbackDto feedbackDto);
}

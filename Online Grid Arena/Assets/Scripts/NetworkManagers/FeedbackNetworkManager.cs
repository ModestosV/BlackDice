using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class FeedbackNetworkManager : AbstractNetworkManager, IFeedbackNetworkManager
{
    public FeedbackNetworkManager() : base(URLs.BASE_URL + "/feedback") { }

    public async Task<IHttpResponseMessage> SendFeedbackAsync(FeedbackDto feedbackDto)
    {
        HttpResponseMessage response = await PostAsync("/send", JsonConvert.SerializeObject(feedbackDto));
        return new HttpResponseMessageAdapter(response);
    }
}
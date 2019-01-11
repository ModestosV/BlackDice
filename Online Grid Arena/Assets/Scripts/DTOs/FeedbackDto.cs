using Newtonsoft.Json;

public class FeedbackDto
{
    [JsonProperty("email")]
    public string Email { get; }
    [JsonProperty("message")]
    public string Message { get; set; }

    public FeedbackDto(string Email, string Message)
    {
        this.Email = Email;
        this.Message = Message;
    }
}
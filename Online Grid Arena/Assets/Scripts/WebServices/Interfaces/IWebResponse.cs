public interface IWebResponse
{
    bool IsNetworkError { get; }
    bool IsHttpError { get; }
    long ResponseCode { get; }
    string ResponseText { get; }
}

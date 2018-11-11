public interface IOnlineMenuPanel
{
    void GetStatus(string responseCode);
    void SetStatus(string statusCode);
    void ClearStatus();
}

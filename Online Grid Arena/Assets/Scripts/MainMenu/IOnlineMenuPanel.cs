public interface IOnlineMenuPanel
{
    void UpdateStatusText(string responseCode);
    void SetStatusText(string statusText);
    void ClearStatus();
}

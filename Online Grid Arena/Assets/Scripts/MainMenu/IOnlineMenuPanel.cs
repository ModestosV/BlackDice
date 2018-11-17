public interface IOnlineMenuPanel
{
    void UpdateStatusText(int responseCode);
    void SetStatusText(string statusText);
    void ClearStatus();
}

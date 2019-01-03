public interface IOnlineMenuPanel
{
    IOnlineMenuController OnlineMenuController { set; }

    void SetStatus(string statusText);
    void ClearStatus();
    void ActivateLoadingCircle();
    void DeactivateLoadingCircle();
}

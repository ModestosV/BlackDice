public interface IPanel
{
    void SetStatus(string statusText);
    void ClearStatus();
    void ActivateLoadingCircle();
    void DeactivateLoadingCircle();
}

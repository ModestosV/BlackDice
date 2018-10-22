public interface IGameManager
{
    ISelectionController SelectionController { get; }
    void QuitApplication();
}

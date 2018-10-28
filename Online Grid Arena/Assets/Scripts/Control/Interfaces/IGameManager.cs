public interface IGameManager : IMonoBehaviour
{
    ISelectionController SelectionController { get; }
    void QuitApplication();
}

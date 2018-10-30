public interface IGameManager : IMonoBehaviour
{
    ISelectionController SelectionController { get; }
    SelectionMode SelectionMode { get; set; }
    void QuitApplication();
}

public interface IGameManager : IMonoBehaviour
{
    SelectionMode SelectionMode { set; }
    void QuitApplication();
}

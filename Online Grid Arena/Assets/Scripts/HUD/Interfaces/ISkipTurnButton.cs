public interface ISkipTurnButton : IMonoBehaviour
{
    SkipTurnButtonController Controller { get; set; }

    void SkipTurn();
}

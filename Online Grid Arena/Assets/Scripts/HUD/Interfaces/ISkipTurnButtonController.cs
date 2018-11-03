public interface ISkipTurnButtonController
{
    ITurnController TurnController { get; set; }

    void SkipTurn();
}

public interface ISelectionController
{
    IGridSelectionController GridSelectionController { set; }
    ITurnController TurnController { set; }

    void Update();
}

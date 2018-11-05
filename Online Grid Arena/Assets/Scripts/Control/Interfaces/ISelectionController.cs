public interface ISelectionController
{
    IHUDController HUDController { get; set; }
    IGridSelectionController GridSelectionController { get; set; }
    ITurnController TurnController { get; set; }

    void Update();
}

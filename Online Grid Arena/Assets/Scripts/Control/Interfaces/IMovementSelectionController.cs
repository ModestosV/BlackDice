public interface IMovementSelectionController
{
    IHUDController HUDController { get; set; }
    IGridSelectionController GridSelectionController { get; set; }
    IGridTraversalController GridTraversalController { get; set; }
    IGameManager GameManager { get; set; }

    void Update();
}

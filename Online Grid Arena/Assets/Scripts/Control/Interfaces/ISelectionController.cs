public interface ISelectionController
{
    IHUDController HUDController { get; set; }
    IGridSelectionController GridSelectionController { get; set; }

    void Update();
}

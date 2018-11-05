public interface IMovementSelectionController
{
    IGridSelectionController GridSelectionController { set; }
    IGameManager GameManager { set; }

    void Update();
}

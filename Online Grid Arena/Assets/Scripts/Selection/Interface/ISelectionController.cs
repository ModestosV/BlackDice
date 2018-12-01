public interface ISelectionController
{
    IGridSelectionController GridSelectionController { set; }
    ITurnController TurnController { set; }
    ISelectionManager SelectionManager { set; }

    void Update(IInputParameters inputParameters);
}

public interface ISelectionController
{
    IGridSelectionController GridSelectionController { set; }
    ISelectionManager SelectionManager { set; }

    void Update(IInputParameters inputParameters);
}

public interface ISelectionController
{
    IGridSelectionController GridSelectionController { set; }

    void Update(IInputParameters inputParameters);
}

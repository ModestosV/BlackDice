public interface IGrid : IMonoBehaviour
{
    GridController GridController { get; set; }

    void InitializeGrid(IGridSelectionController gridSelectionController);
}

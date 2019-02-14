public class UpdateSelectionModeEvent : AbstractEvent
{
    public SelectionMode SelectionMode { get; }

    public UpdateSelectionModeEvent(SelectionMode selectionMode)
    {
        SelectionMode = selectionMode;
    }
}
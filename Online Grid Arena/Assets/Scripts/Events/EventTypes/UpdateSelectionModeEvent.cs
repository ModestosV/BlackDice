public class UpdateSelectionModeEvent : IEvent
{
    public SelectionMode SelectionMode { get; }

    public UpdateSelectionModeEvent(SelectionMode selectionMode)
    {
        SelectionMode = selectionMode;
    }
}
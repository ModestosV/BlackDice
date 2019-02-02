public sealed class FreeSelectionController : AbstractSelectionController
{
    public FreeSelectionController(IGridSelectionController gridSelectionController) :base(gridSelectionController)
    {
    }

    protected override void DoFirst()
    {
        gridSelectionController.BlurAll();
    }

    protected override void DoClickDisabledTile()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        EventBus.Publish(new SelectTileEvent(inputParameters.TargetTile));
    }

    protected override void DoClickSelectedTile()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
    }

    protected override void DoClickOccupiedOtherTile()
    {
        EventBus.Publish(new SelectTileEvent(inputParameters.TargetTile));
    }

    protected override void DoHoverUnoccupiedTile()
    {
        inputParameters.TargetTile.Hover();
    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.Hover();
    }

    protected override void DoHoverAbility()
    {
        
    }
}

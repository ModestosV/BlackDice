public sealed class FreeSelectionController : AbstractSelectionController
{
    private ITurnController turnController;

    public FreeSelectionController(ITurnController turnController, IGridSelectionController gridSelectionController) :base(gridSelectionController)
    {
        this.turnController = turnController;
    }

    protected override void DoFirst()
    {
        gridSelectionController.BlurAll();
    }

    protected override void DoTabPressed()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
        turnController.SelectActiveCharacter();
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

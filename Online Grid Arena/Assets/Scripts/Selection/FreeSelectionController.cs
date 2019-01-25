﻿public sealed class FreeSelectionController : AbstractSelectionController
{
    public TurnController TurnController { private get; set; }

    protected override void DoFirst()
    {
        GridSelectionController.BlurAll();
    }

    protected override void DoEscapePressed()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
    }

    protected override void DoTabPressed()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
        TurnController.SelectActiveCharacter();
    }

    protected override void DoClickOffGrid()
    {
        EventBus.Publish(new DeselectSelectedTileEvent());
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

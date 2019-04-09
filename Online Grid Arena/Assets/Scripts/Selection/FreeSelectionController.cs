using UnityEngine;

public sealed class FreeSelectionController : AbstractSelectionController
{
    public FreeSelectionController(IGridSelectionController gridSelectionController) :base(gridSelectionController)
    {
    }

    protected override void DoFirst()
    {
        gridSelectionController.BlurAll();
        gridSelectionController.DehighlightAll();
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
        Debug.Log("Selected tile clicked");
        var occupantCharacter = inputParameters.TargetTile.OccupantCharacter;
        if (occupantCharacter != null)
        {
            if(occupantCharacter.IsActive)
            {
                EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.MOVEMENT));
            }
        }
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

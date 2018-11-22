﻿public sealed class FreeSelectionController : SelectionController
{
    protected override void DoFirst()
    {
        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();
    }

    protected override void DoEscapePressed()
    {
        GridSelectionController.DeselectAll();
    }

    protected override void DoTabPressed()
    {
        GridSelectionController.DeselectAll();
        TurnController.SelectActiveCharacter();
    }

    protected override void DoClickOffGrid()
    {
        GridSelectionController.DeselectAll();
    }

    protected override void DoHoverOffGrid()
    {

    }

    protected override void DoClickDisabledTile()
    {
        GridSelectionController.DeselectAll();
    }

    protected override void DoHoverDisabledTile()
    {

    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        GridSelectionController.DeselectAll();
        inputParameters.TargetTile.Select();
    }

    protected override void DoClickSelectedTile()
    {
        inputParameters.TargetTile.Deselect();
    }

    protected override void DoClickOccupiedOtherTile()
    {
        GridSelectionController.DeselectAll();
        inputParameters.TargetTile.Select();
    }

    protected override void DoHoverUnoccupiedTile()
    {
        inputParameters.TargetTile.Hover();
    }

    protected override void DoHoverSelectedTile()
    {

    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.Hover();
    }
}
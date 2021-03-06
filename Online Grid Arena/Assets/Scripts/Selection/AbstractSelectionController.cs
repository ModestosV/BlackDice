﻿public abstract class AbstractSelectionController : ISelectionController
{
    protected readonly IGridSelectionController gridSelectionController;
    protected IInputParameters inputParameters;

    protected AbstractSelectionController(IGridSelectionController gridSelectionController)
    {
        this.gridSelectionController = gridSelectionController;
    }

    public void UpdateSelection(IInputParameters inputParameters)
    {
        this.inputParameters = inputParameters;

        DoFirst();
        
        if(inputParameters.IsRightClickDown)
        {
            DoRightClickPressed();
            return;
        }

        if (inputParameters.IsKeyEscapeDown)
        {
            DoEscapePressed();
            return;
        }
        
        if (inputParameters.IsKeyTabDown)
        {
            DoTabPressed();
            return;
        }
        
        if (!inputParameters.IsMouseOverGrid)
        {
            return;
        }

        // Invariant: Mouse is over grid
        bool tileIsEnabled = inputParameters.TargetTile.IsEnabled;

        if (!tileIsEnabled && inputParameters.IsLeftClickDown)
        {
            DoClickDisabledTile();
            return;
        }

        // Hovered over disabled tile
        if (!tileIsEnabled)
        {
            DoHoverDisabledTile();
            return;
        }

        // Invariant: Target tile is enabled

        bool tileIsOccupied = inputParameters.TargetTile.IsOccupied();
        bool tileIsCurrentSelectedTile = gridSelectionController.IsSelectedTile(inputParameters.TargetTile);

        if (inputParameters.TargetTile.IsObstructed) return;

        if (inputParameters.IsLeftClickDown && !tileIsOccupied && !tileIsCurrentSelectedTile)
        {
            DoClickUnoccupiedOtherTile();
            return;
        }
        
        if (inputParameters.IsLeftClickDown && tileIsCurrentSelectedTile)
        {
            DoClickSelectedTile();
            return;
        }
        
        if (inputParameters.IsLeftClickDown)
        {
            DoClickOccupiedOtherTile();
            return;
        }
        
        if (!tileIsOccupied)
        {
            DoHoverUnoccupiedTile();
            return;
        }

        if (tileIsCurrentSelectedTile)
        {
            DoHoverSelectedTile();
            return;
        }

        DoHoverOccupiedTile();
    }

    private void DoRightClickPressed()
    {
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
    }

    private void DoEscapePressed()
    {
        EventBus.Publish(new EscapePressedEvent());
    }

    private void DoTabPressed()
    {
        EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
        EventBus.Publish(new ActiveCharacterEvent());
    }

    protected virtual void DoFirst() { }
    protected virtual void DoClickDisabledTile() { }
    protected virtual void DoHoverDisabledTile() { }
    protected virtual void DoClickUnoccupiedOtherTile() { }
    protected virtual void DoClickSelectedTile() { }
    protected virtual void DoClickOccupiedOtherTile() { }
    protected virtual void DoHoverUnoccupiedTile() { }
    protected virtual void DoHoverSelectedTile() { }
    protected virtual void DoHoverOccupiedTile() { }
    protected virtual void DoHoverAbility() { }
}

public abstract class SelectionController : ISelectionController
{
    public IGridSelectionController GridSelectionController { protected get; set; }
    public ITurnController TurnController { protected get; set; }
    public ISelectionManager SelectionManager { protected get; set; }

    protected IInputParameters inputParameters;

    protected abstract void DoFirst();
    protected abstract void DoEscapePressed();
    protected abstract void DoTabPressed();
    protected abstract void DoClickOffGrid();
    protected abstract void DoHoverOffGrid();
    protected abstract void DoClickDisabledTile();
    protected abstract void DoHoverDisabledTile();
    protected abstract void DoClickUnoccupiedOtherTile();
    protected abstract void DoClickSelectedTile();
    protected abstract void DoClickOccupiedOtherTile();
    protected abstract void DoHoverUnoccupiedTile();
    protected abstract void DoHoverSelectedTile();
    protected abstract void DoHoverOccupiedTile();

    public void Update(IInputParameters inputParameters)
    {
        this.inputParameters = inputParameters;

        DoFirst();
        
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
        
        if (!inputParameters.IsMouseOverGrid && inputParameters.IsLeftClickDown)
        {
            DoClickOffGrid();
            return;
        }

        if (!inputParameters.IsMouseOverGrid)
        {
            DoHoverOffGrid();
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
        bool tileIsCurrentSelectedTile = GridSelectionController.IsSelectedTile(inputParameters.TargetTile);
        
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
}

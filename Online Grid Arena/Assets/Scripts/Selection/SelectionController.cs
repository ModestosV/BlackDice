public abstract class SelectionController : ISelectionController
{
    public IGridSelectionController GridSelectionController { protected get; set; }
    public ITurnController TurnController { protected get; set; }
    public ISelectionManager SelectionManager { protected get; set; }

    protected IInputParameters inputParameters;

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

    protected virtual void DoFirst()
    {

    }

    protected virtual void DoEscapePressed()
    {

    }

    protected virtual void DoTabPressed()
    {

    }

    protected virtual void DoClickOffGrid()
    {

    }

    protected virtual void DoHoverOffGrid()
    {

    }

    protected virtual void DoClickDisabledTile()
    {

    }

    protected virtual void DoHoverDisabledTile()
    {

    }

    protected virtual void DoClickUnoccupiedOtherTile()
    {

    }

    protected virtual void DoClickSelectedTile()
    {

    }

    protected virtual void DoClickOccupiedOtherTile()
    {

    }

    protected virtual void DoHoverUnoccupiedTile()
    {

    }

    protected virtual void DoHoverSelectedTile()
    {

    }

    protected virtual void DoHoverOccupiedTile()
    {

    }
}

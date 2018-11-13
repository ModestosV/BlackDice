public class AttackAbilitySelectionController : InputController, IAbilitySelectionController {

    public IGridSelectionController GridSelectionController { protected get; set; }
    public IGameManager GameManager { protected get; set; }

    public int ActiveAbilityNumber { protected get; set; }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        GridSelectionController.BlurAll();
        GridSelectionController.DehighlightAll();

        // Escape buton pressed
        if (InputParameters.IsKeyEscapeDown)
        {
            GameManager.SelectionMode = SelectionMode.SELECTION;
            return;
        }

        // Clicked off grid
        if (!InputParameters.IsMouseOverGrid && InputParameters.IsLeftClickDown)
        {
            return;
        }

        // Hovered off grid
        if (!InputParameters.IsMouseOverGrid)
        {
            return;
        }

        // Invariant: Mouse is over grid

        bool tileIsEnabled = InputParameters.TargetTile.IsEnabled;

        // Clicked on disabled tile
        if (!tileIsEnabled && InputParameters.IsLeftClickDown)
        {
            return;
        }

        // Hovered over disabled tile
        if (!tileIsEnabled)
        {
            return;
        }

        // Invariant: Target tile is enabled

        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        ICharacterController targetCharacter = InputParameters.TargetTile.OccupantCharacter;

        bool tileIsOccupied = InputParameters.TargetTile.IsOccupied();
        bool tileIsCurrentSelectedTile = GridSelectionController.IsSelectedTile(InputParameters.TargetTile);

        // Clicked unoccupied tile
        if (InputParameters.IsLeftClickDown && !tileIsOccupied)
        {
            return;
        }

        // Clicked occupied other tile
        if (InputParameters.IsLeftClickDown && !tileIsCurrentSelectedTile)
        {
            selectedCharacter.ExecuteAbility(ActiveAbilityNumber, targetCharacter);
            GameManager.SelectionMode = SelectionMode.SELECTION;
            return;
        }

        // Invariant: Left-click is not down

        // Hovered over unoccupied tile
        if (!tileIsOccupied)
        {
            InputParameters.TargetTile.HoverError();
            return;
        }

        // Hovered over occupied tile
        InputParameters.TargetTile.Highlight();
        return;
    }
}

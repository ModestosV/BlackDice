public class HealAbilitySelectionController : InputController, IAbilitySelectionController
{
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
        bool targetCharacterIsAlly = targetCharacter != null ? selectedCharacter.IsAlly(targetCharacter) : false;

        bool tileIsOccupied = InputParameters.TargetTile.IsOccupied();
        bool tileIsCurrentSelectedTile = GridSelectionController.IsSelectedTile(InputParameters.TargetTile);

        // Clicked unoccupied tile
        if (InputParameters.IsLeftClickDown && !tileIsOccupied)
        {
            return;
        }

        // Clicked ally occupied tile
        if (InputParameters.IsLeftClickDown && targetCharacterIsAlly)
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

        // Hovered over ally occupied tile
        if (targetCharacterIsAlly)
        {
            InputParameters.TargetTile.Highlight();
            return;
        }

        // Hovered over enemy occupied tile
        InputParameters.TargetTile.HoverError();
        return;
    }
}

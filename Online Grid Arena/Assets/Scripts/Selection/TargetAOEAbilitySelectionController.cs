using System.Collections.Generic;

public sealed class TargetAOEAbilitySelectionController : AbstractTileAbilitySelectionController
{
    public TargetAOEAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected override void DoClickUnoccupiedOtherTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            List<IHexTileController> target = new List<IHexTileController>();
            target.Add(inputParameters.TargetTile);

            selectedCharacter.ExecuteAbility(activeAbilityIndex, target);
            EventBus.Publish(new UpdateSelectionModeEvent(SelectionMode.FREE));
            return;
        }
    }

    protected override void DoHoverUnoccupiedTile()
    {
        ICharacterController selectedCharacter = gridSelectionController.GetSelectedCharacter();

        IHexTileController selectedTile = gridSelectionController.SelectedTile;
        int distance = selectedTile.GetAbsoluteDistance(inputParameters.TargetTile);
        bool inRange = selectedCharacter.IsAbilityInRange(activeAbilityIndex, distance);

        if (inRange)
        {
            inputParameters.TargetTile.Hover(HoverType.HEAL);
            foreach (IHexTileController affected in inputParameters.TargetTile.GetNeighbors())
            {
                affected.Hover(HoverType.HEAL);
            }
        }
        else
        {
            inputParameters.TargetTile.HoverError();
        }
    }

    protected override void DoHoverSelectedTile()
    {
        DoHoverUnoccupiedTile();
    }

    protected override void DoHoverOccupiedTile()
    {
        DoHoverUnoccupiedTile();
    }
}

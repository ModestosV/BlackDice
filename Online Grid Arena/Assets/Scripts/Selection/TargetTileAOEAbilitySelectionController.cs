using System.Collections.Generic;
using UnityEngine;

public sealed class TargetTileAOEAbilitySelectionController : AbstractTileAbilitySelectionController
{
    public TargetTileAOEAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
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
            inputParameters.TargetTile.Highlight();
            foreach (IHexTileController affected in inputParameters.TargetTile.GetNeighbors())
            {
                affected.Hover(HoverType.DAMAGE);
            }
        }
        else
        {
            inputParameters.TargetTile.HoverError();
        }
    }

    protected override void DoHoverSelectedTile()
    {
        inputParameters.TargetTile.Hover(HoverType.INVALID);
    }

    protected override void DoHoverOccupiedTile()
    {
        inputParameters.TargetTile.Hover(HoverType.INVALID);
    }
}

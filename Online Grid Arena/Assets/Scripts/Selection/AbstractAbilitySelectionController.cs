public abstract class AbstractAbilitySelectionController : AbstractSelectionController
{
    protected int activeAbilityIndex;
    
    protected AbstractAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    protected void SetActiveAbility()
    {
        if (!(inputParameters.GetAbilityNumber() > -1)) return;

        activeAbilityIndex = inputParameters.GetAbilityNumber();
    }
}

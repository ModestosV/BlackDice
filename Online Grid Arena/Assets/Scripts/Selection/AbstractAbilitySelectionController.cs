public abstract class AbstractAbilitySelectionController : AbstractSelectionController
{
    protected int activeAbilityIndex;
    
    protected void SetActiveAbility()
    {
        if (!(inputParameters.GetAbilityNumber() > -1)) return;

        activeAbilityIndex = inputParameters.GetAbilityNumber();
    }
}

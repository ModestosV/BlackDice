public abstract class AbstractAbilitySelectionController : AbstractSelectionController
{
    protected int activeAbilityIndex;
    
    protected AbstractAbilitySelectionController(IGridSelectionController gridSelectionController) : base(gridSelectionController)
    {

    }

    private void SetActiveAbility()
    {
        if (!(inputParameters.GetAbilityNumber() > -1)) return;

        activeAbilityIndex = inputParameters.GetAbilityNumber();
    }

    protected override void DoFirst()
    {
        SetActiveAbility();
        gridSelectionController.BlurAll();
        gridSelectionController.DehighlightAll();
    }
}

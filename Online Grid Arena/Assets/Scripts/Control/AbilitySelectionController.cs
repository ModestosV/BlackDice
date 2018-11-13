public class AbilitySelectionController : InputController
{
    public IGridSelectionController GridSelectionController { protected get; set; }

    public IAbilitySelectionController AttackAbilitySelectionController { protected get; set; }
    public IAbilitySelectionController HealAbilitySelectionController { protected get; set; }

    private AbilityType activeAbilityType;
    
    private void SetActiveAbility()
    {
        if (!(InputParameters.GetAbilityNumber() > -1)) return;

        int activeAbilityNumber = InputParameters.GetAbilityNumber();
        AttackAbilitySelectionController.ActiveAbilityNumber = activeAbilityNumber;
        HealAbilitySelectionController.ActiveAbilityNumber = activeAbilityNumber;

        ICharacterController selectedCharacter = GridSelectionController.GetSelectedCharacter();
        activeAbilityType = selectedCharacter.GetAbilityType(activeAbilityNumber);
    }

    private void UpdateInputParameters()
    {
        AttackAbilitySelectionController.InputParameters = InputParameters;
        HealAbilitySelectionController.InputParameters = InputParameters;
    }

    public override void Update()
    {
        if (DebounceUpdate())
            return;

        UpdateInputParameters();

        SetActiveAbility();

        switch (activeAbilityType)
        {
            case AbilityType.ATTACK:
                AttackAbilitySelectionController.Update();
                break;
            case AbilityType.HEAL:
                HealAbilitySelectionController.Update();
                break;
        }
    }
}

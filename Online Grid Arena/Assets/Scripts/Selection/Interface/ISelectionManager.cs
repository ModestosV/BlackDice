public interface ISelectionManager
{
    IGridSelectionController GridSelectionController { get; set; }

    void Update(IInputParameters inputParameters);

    bool SelectedCharacterCanUseAbility(int abilityIndex);
}

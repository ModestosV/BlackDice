public interface ISelectionManager
{
    void UpdateSelectionMode(IInputParameters inputParameters);

    bool SelectedCharacterCanUseAbility(int abilityIndex);
}

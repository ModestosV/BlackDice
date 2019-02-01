public interface ISelectionManager
{
    void Update(IInputParameters inputParameters);

    bool SelectedCharacterCanUseAbility(int abilityIndex);
}

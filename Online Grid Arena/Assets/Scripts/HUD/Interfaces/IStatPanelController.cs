public interface IStatPanelController
{
    void SetCharacter(ICharacter selectedCharacter);
    void UpdateStatValues();
    void UpdateStatNames();
    void DisableStatDisplays();
    void EnableStatDisplays();
}

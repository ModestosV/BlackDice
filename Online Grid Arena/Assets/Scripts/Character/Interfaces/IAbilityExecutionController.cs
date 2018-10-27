public interface IAbilityExecutionController
{
    IStatPanel StatPanel { get; set; }

    void ExecuteAbility(IAbility ability, ICharacter targetCharacter);
}

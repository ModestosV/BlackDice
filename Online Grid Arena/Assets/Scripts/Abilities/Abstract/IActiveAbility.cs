public interface IActiveAbility : IAbility
{
    int CooldownRemaining { get; }

    bool IsOnCooldown();
    void UpdateCooldown();
}
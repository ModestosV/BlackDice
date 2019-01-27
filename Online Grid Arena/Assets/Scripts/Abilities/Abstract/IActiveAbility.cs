public interface IActiveAbility : IAbility
{
    int Cooldown { get; }
    int CooldownRemaining { get; }

    bool IsOnCooldown();
    void UpdateCooldown();
}
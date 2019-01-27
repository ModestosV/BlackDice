public interface IActiveAbility : IAbility
{
    int Cooldown { get; set; }
    int CooldownRemaining { get; set; }

    bool IsOnCooldown();
    void UpdateCooldown();
}
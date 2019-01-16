public interface IActiveAbility : IAbility
{
    bool IsOnCooldown();
    void UpdateCooldown();
}
public interface ITargetedAbility : IActiveAbility
{
    AbilityType Type { get; }

    bool IsInRange(int range);
}
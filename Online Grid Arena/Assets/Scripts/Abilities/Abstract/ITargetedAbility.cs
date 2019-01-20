public interface ITargetedAbility : IActiveAbility
{
    AbilityType Type { get; set; }

    bool IsInRange(int range);
}
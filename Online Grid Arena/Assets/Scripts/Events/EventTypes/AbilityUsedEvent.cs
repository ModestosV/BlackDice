public class AbilityUsedEvent : AbstractEvent
{
    public int AbilityIndex { get; }

    public AbilityUsedEvent(int abilityIndex)
    {
        AbilityIndex = abilityIndex;
    }
}
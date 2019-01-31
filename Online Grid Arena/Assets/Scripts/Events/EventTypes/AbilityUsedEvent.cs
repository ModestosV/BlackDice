public class AbilityUsedEvent : IEvent
{
    public int AbilityIndex { get; }

    public AbilityUsedEvent(int abilityIndex)
    {
        AbilityIndex = abilityIndex;
    }
}
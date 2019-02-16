public class AbilitySelectedEvent : AbstractEvent
{
    public int AbilityIndex { get; }

    public AbilitySelectedEvent(int abilityIndex)
    {
        AbilityIndex = abilityIndex;
    }
}
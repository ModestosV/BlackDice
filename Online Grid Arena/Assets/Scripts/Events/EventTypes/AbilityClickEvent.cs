public class AbilityClickEvent : IEvent
{
    public int AbilityIndex { get; }

    public AbilityClickEvent(int abilityIndex)
    {
        AbilityIndex = abilityIndex;
    }
}
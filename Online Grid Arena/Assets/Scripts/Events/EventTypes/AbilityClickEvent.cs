public class AbilityClickEvent : AbstractEvent
{
    public int AbilityIndex { get; }

    public AbilityClickEvent(int abilityIndex)
    {
        AbilityIndex = abilityIndex;
    }
}
public abstract class PassiveAbility : AbstractAbility
{
    protected IEffect effect;

    protected PassiveAbility(AbilityType type, IEffect effect) : base(type, 0)
    {
        this.effect = effect;
    }

    public abstract void ActivatePassive();
}

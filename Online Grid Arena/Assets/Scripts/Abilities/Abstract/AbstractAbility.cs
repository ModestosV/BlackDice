using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractAbility : IAbility
{
    public List<IEffect> Effects { get; }
    public Sprite AbilityIcon { get; }
    public string Description { get; }

    protected readonly ICharacter character;
    protected readonly IActionHandler actionHandler = new ActionHandler();

    protected AbstractAbility(Sprite abilityIcon, ICharacter character, string description)
    {
        AbilityIcon = abilityIcon;
        this.character = character;
        Effects = new List<IEffect>();
        Description = description;
    }

    protected abstract void PrimaryAction(List<IHexTileController> targetTiles);
    protected virtual void SecondaryAction(List<IHexTileController> targetTiles) { }

    public virtual void Execute(List<IHexTileController> targetTiles)
    {
        Debug.Log(ToString() + " Ability Execute()");

        PrimaryAction(targetTiles);
        SecondaryAction(targetTiles);
    }

    public void AddEffect(IEffect effect)
    {
        Effects.Add(effect);
    }
}

using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractAbility : IAbility
{
    public List<IEffect> Effects { get; set; }
    public Sprite AbilityIcon { get; set; }

    protected readonly ICharacter character;

    protected AbstractAbility(Sprite abilityIcon, ICharacter character)
    {
        AbilityIcon = abilityIcon;
        this.character = character;
    }

    protected abstract void PrimaryAction(List<IHexTileController> targetTiles);
    protected virtual void SecondaryAction(List<IHexTileController> targetTiles) { }

    public void Execute(List<IHexTileController> targetTiles)
    {
        PrimaryAction(targetTiles);

        SecondaryAction(targetTiles);
    }

    public void AddEffect(IEffect effect)
    {
        Effects.Add(effect);
    }
}

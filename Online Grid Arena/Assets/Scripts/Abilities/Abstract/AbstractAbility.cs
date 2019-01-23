﻿using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractAbility : IAbility
{
    public List<IEffect> Effects { get; set; }
    public Sprite AbilityIcon { get; set; }
    public string Description { get; protected set; }

    protected readonly ICharacter character;

    protected AbstractAbility(Sprite abilityIcon, ICharacter character)
    {
        AbilityIcon = abilityIcon;
        this.character = character;
        Effects = new List<IEffect>();
        Description = "Default ability description. If you're seeing this, somebody didn't do their job right";
    }

    protected abstract void PrimaryAction(List<IHexTileController> targetTiles);
    protected virtual void SecondaryAction(List<IHexTileController> targetTiles) { }

    public virtual void Execute(List<IHexTileController> targetTiles)
    {
        PrimaryAction(targetTiles);
        SecondaryAction(targetTiles);
        EventBus.Publish(new SelectTileEvent(character.Controller.OccupiedTile));
    }

    public void AddEffect(IEffect effect)
    {
        Effects.Add(effect);
    }
}

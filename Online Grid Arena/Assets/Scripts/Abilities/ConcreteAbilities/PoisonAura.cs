using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class PoisonAura : PassiveAbility
{
    private readonly GameObject animationPrefab;
    private readonly AudioClip soundEffect;

    public PoisonAura(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/poison"),
        activeCharacter,
        "Poison Aura - Passive Ability \nAt the end of his turn, Agent Frog releases a cloud of poison that damages all enemies twice in a 1 tile radius for 50% of his attack each time.",
        true)
    {
        animationPrefab = Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation");
        soundEffect = Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker");
    }

    protected override async void PrimaryAction(List<IHexTileController> targetTile)
    {
        Debug.Log("Casting Poison Aura. Primary action being called.");
        List<IHexTileController> neighbors = new List<IHexTileController>();
        foreach (IHexTileController tile in this.character.Controller.OccupiedTile.GetNeighbors())
        {
            neighbors.Add(tile);
        }
        DamageTiles(neighbors);
        await Task.Delay(500);
        DamageTiles(neighbors);
    }

    private void DamageTiles(List<IHexTileController> targetTiles)
    {
        EventBus.Publish(new AbilitySoundEvent(soundEffect));
        foreach (IHexTileController tile in targetTiles)
        {
            tile.PlayAbilityAnimation(animationPrefab);
            if (tile.OccupantCharacter != null && !(tile.OccupantCharacter.IsAlly(this.character.Controller)))
            {
                actionHandler.Damage(character.Controller.CharacterStats["attack"].Value*0.5f, tile.OccupantCharacter);
            }
        }
    }
}

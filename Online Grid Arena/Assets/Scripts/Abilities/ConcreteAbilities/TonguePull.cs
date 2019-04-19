using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class TonguePull : AbstractTargetedAbility
{
    public TonguePull(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/tonguePull"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/frog sound effect"),
        activeCharacter,
        3,
        6,
        AbilityType.TARGET_CHARACTER_LINE,
        "Tongue Pull - Special Ability \nAgent Frog pulls an opponent or ally towards himself using his tongue. Damages opponents only, for 100% of his attack value. Range: 6")
    { }

    protected override async void PrimaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Tongue Pull. Primary action being called.");
        if (targetTiles.Count > 1)
        {
            if (!targetTiles[1].OccupantCharacter.IsAlly(character.Controller))
            {
                DamageLastTile(targetTiles);
            }
        }
        PlaySoundEffect();
        await Task.Delay(300);
    }

    protected override void SecondaryAction(List<IHexTileController> targetTiles)
    {
        Debug.Log("Casting Tongue Pull. Secondary action being called.");

        if (targetTiles.Count > 1)
        {
            if (targetTiles[1].OccupantCharacter != null)
            {
                ICharacterController target = targetTiles[1].OccupantCharacter;
                target.OccupiedTile.OccupantCharacter = null;

                target.Character.MoveToTile(targetTiles[2].HexTile);
                target.OccupiedTile = targetTiles[2];
                targetTiles[2].OccupantCharacter = target;
            }
        }
    }

    private void DamageLastTile(List<IHexTileController> targetTiles)
    {
        if (!targetTiles[1].OccupantCharacter.IsAlly(character.Controller))
        {
            actionHandler.Damage(character.Controller.CharacterStats["attack"].Value, targetTiles[1].OccupantCharacter);
            PlayAnimation(targetTiles[1]);
        }
    }
}

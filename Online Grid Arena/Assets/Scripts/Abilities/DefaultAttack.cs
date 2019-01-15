using UnityEngine;

public sealed class DefaultAttack : TargetedAbility
{
    public DefaultAttack(ICharacter activeCharacter) : base(
        AbilityType.TARGET_ENEMY,
        1,
        5,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefaultAttackAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/MLG_Hitmarker"),
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        activeCharacter
        )
    {

    }

    protected override void PrimaryAction(IHexTileController targetTile)
    {
        targetTile.Damage(activeCharacter.Controller.CharacterStats["attack"].CurrentValue-5);
        PlaySoundEffect();
        PlayAnimation(targetTile);
    }
}
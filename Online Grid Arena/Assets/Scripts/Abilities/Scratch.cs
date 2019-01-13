using UnityEngine;

public sealed class Scratch : TargetedAbility
{
    public Scratch(RocketCat activeCharacter) : base(
        AbilityType.TARGET_ENEMY,
        1,
        25.0f,
        1,
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/CottonRip"),
        Resources.Load<Sprite>("Sprites/cursorSword_gold"),
        activeCharacter
        )
    {

    }
}

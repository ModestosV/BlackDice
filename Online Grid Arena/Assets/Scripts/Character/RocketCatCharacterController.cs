using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCatCharacterController : CharacterController
{
    public RocketCatCharacterController()
    {
        // Init abilities
        GameObject scratchAbilityAnimationPrefab = Resources.Load<GameObject>("Prefabs/AbilityAnimations/Scratch");
        AudioClip scratchAbilitySound = Resources.Load<AudioClip>("Audio/Ability/CottonRip");

        IAbility scratch = new BasicAttackAbility(25.0f, 1, 1, scratchAbilityAnimationPrefab, scratchAbilitySound);

        Abilities = new List<IAbility>() { scratch };

        // Init stats

        ICharacterStat health = new CharacterStat(120.0f);
        ICharacterStat moves = new CharacterStat(6.0f);

        CharacterStats = new Dictionary<string, ICharacterStat>()
        {
            {"health", health},
            {"moves", moves}
        };
    }
}

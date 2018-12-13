using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCatCharacterController : CharacterController
{
    public RocketCatCharacterController()
    {
        // Init abilities
        GameObject scratchAbilityAnimationPrefab = (GameObject)Resources.Load("Prefabs/AbilityAnimations/Scratch", typeof(GameObject));
        IAbility scratch = new BasicAttackAbility(25.0f, 1.0f, scratchAbilityAnimationPrefab);

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

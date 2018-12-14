using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCat : Character
{
    void Awake()
    {
        // Init abilities

        GameObject scratchAbilityAnimationPrefab = Resources.Load<GameObject>("Prefabs/AbilityAnimations/ScratchAnimation");
        AudioClip scratchAbilitySound = Resources.Load<AudioClip>("Audio/Ability/CottonRip");
        IAbility scratch = new BasicAttackAbility(25.0f, 1, 1, scratchAbilityAnimationPrefab, scratchAbilitySound);

        var abilities = new List<IAbility>() { scratch };

        // Init stats

        ICharacterStat health = new CharacterStat(120.0f);
        ICharacterStat moves = new CharacterStat(6.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            {"health", health},
            {"moves", moves}
        };

        characterController = new CharacterController()
        {
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = GetComponentInChildren<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats
        };
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
    }
}

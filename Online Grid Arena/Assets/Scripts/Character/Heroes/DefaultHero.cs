using System.Collections.Generic;
using UnityEngine;

public sealed class DefaultHero : AbstractCharacter
{
    void Awake()
    {
        // Init abilities

        IAbility basicAttack = new BasicAttackAbility(20.0f, 5, 1, null, null);

        IAbility basicHeal = new BasicHealAbility(15.0f, 5, 1, null, null);

        var abilities = new List<IAbility>() { basicAttack, basicHeal };

        // Init stats

        ICharacterStat health = new CharacterStat(100.0f);
        ICharacterStat moves = new CharacterStat(5.0f);

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

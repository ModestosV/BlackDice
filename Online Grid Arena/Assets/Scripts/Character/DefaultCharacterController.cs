using System.Collections.Generic;
using UnityEngine;

public sealed class DefaultCharacterController : CharacterController
{
    public DefaultCharacterController()
    {
        // Init abilities

        IAbility basicAttack = new BasicAttackAbility(20.0f, 5);

        IAbility basicHeal = new BasicHealAbility(15.0f, 5);

        Abilities = new List<IAbility>() { basicAttack, basicHeal };

        // Init stats

        ICharacterStat health = new CharacterStat(100.0f);
        ICharacterStat moves = new CharacterStat(5.0f);

        CharacterStats = new Dictionary<string, ICharacterStat>()
        {
            {"health", health},
            {"moves", moves}
        };
    }
}

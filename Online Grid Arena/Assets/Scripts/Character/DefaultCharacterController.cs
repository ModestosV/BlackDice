using System.Collections.Generic;

public sealed class DefaultCharacterController : CharacterController
{
    public DefaultCharacterController()
    {
        // Init abilities
        
        IAbility basicAttack = new BasicAttackAbility()
        {
            Type = AbilityType.TARGET_ENEMY,
            Values = new Dictionary<string, float>()
            {
                {"power", 20},
            }
        };

        IAbility basicHeal = new BasicHealAbility()
        {
            Type = AbilityType.TARGET_ALLY,
            Values = new Dictionary<string, float>()
            {
                {"power", 15},
            }
        };

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

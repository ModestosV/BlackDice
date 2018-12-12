using System.Collections.Generic;

public sealed class RocketCatCharacterController : CharacterController
{
    public RocketCatCharacterController()
    {
        // Init abilities

        IAbility scratch = new BasicAttackAbility()
        {
            Type = AbilityType.TARGET_ENEMY,
            Values = new Dictionary<string, float>()
            {
                {"power", 25},
                {"range", 1}
            }
        };

        Abilities = new List<IAbility>() { scratch};

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

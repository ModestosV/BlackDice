using System.Collections.Generic;
using UnityEngine;

public sealed class DefaultCharacter : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        // Init abilities

        IAbility defaultAttack = new DefaultAttack(this);
        IAbility defaultHeal = new DefaultHeal(this);
        IAbility placeholder = new Placeholder(this);

        var abilities = new List<IAbility>() { defaultAttack, defaultHeal, placeholder, placeholder };

        var effects = new List<IEffect>() { };

        // Init stats

        ICharacterStat health = new CharacterStat(100.0f);
        ICharacterStat moves = new CharacterStat(5.0f);
        ICharacterStat attack = new CharacterStat(20.0f);
        ICharacterStat defense = new CharacterStat(100.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "health", health },
            { "moves", moves },
            { "attack", attack },
            { "defense", defense }
        };

        characterController = new CharacterController()
        {
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = GetComponentInChildren<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            ActiveCircle = GetComponentInChildren<ActiveCircle>().GetComponentInChildren<SpriteRenderer>()
        };
    }
}

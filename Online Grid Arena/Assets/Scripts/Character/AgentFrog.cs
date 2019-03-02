using System.Collections.Generic;
using UnityEngine;

public sealed class AgentFrog : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        ICharacterStat health = new CharacterStat(150.0f);
        ICharacterStat moves = new CharacterStat(3.0f);
        ICharacterStat attack = new CharacterStat(30.0f);
        ICharacterStat defense = new CharacterStat(100.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            { "health", health },
            { "moves", moves },
            { "attack", attack },
            { "defense", defense }
        };
        
        IAbility lick = new Lick(this);
        IAbility hop = new Hop(this);
        IAbility tonguePull = new TonguePull(this);
        IAbility poisonAuraAbility = new PoisonAura(this);

        var abilities = new List<IAbility>() { lick, hop, tonguePull, poisonAuraAbility };
        var effects = new List<IEffect>() { };

        characterController = new CharacterController(this)
        {
            CharacterOwner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = healthBar.GetComponent<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            ActiveCircle = activeCircle.GetComponent<SpriteRenderer>()
        };
    }
}

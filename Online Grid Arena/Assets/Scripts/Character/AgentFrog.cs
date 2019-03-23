using System.Collections.Generic;
using UnityEngine;

public sealed class AgentFrog : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        var characterStats = InitializeStats(150, 3, 30, 100);

        IAbility lick = new Lick(this);
        IAbility hop = new Hop(this);
        IAbility tonguePull = new TonguePull(this);
        IAbility poisonAuraAbility = new PoisonAura(this);

        var abilities = new List<IAbility>() { lick, hop, tonguePull, poisonAuraAbility };

        characterController = new CharacterController(this)
        {
            Owner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = healthBar.GetComponent<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            Shield = shield.GetComponent<MeshRenderer>()
        };
    }
}

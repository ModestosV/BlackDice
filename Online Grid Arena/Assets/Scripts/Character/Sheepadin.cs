using System.Collections.Generic;
using UnityEngine;

public sealed class Sheepadin : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        IAbility placeholder = new Placeholder(this);
        WoolArmorEffect woolArmorEffect = new WoolArmorEffect();
        IAbility woolArmor = new WoolArmor(this, woolArmorEffect);
        IAbility headbutt = new Headbutt(this);
        IAbility holyBah = new HolyBah(this, woolArmorEffect);


        var abilities = new List<IAbility>() { headbutt, holyBah, woolArmor, placeholder };
        var effects = new List<IEffect>() { };

        var characterStats = InitializeStats(120, 20, 20, 100);

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

using System.Collections.Generic;
using UnityEngine;

public sealed class Sheepadin : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        transform.GetChild(1).transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

        WoolArmorEffect woolArmorEffect = new WoolArmorEffect();
        IAbility woolArmor = new WoolArmor(this, woolArmorEffect);
        IAbility headbutt = new Headbutt(this, woolArmorEffect);
        IAbility holyShear = new HolyShear(this, woolArmorEffect);
        IAbility bahtteryAssault = new BahtteryAssault(this, woolArmorEffect);

        var abilities = new List<IAbility>() { headbutt, holyShear, woolArmor, bahtteryAssault };

        var characterStats = InitializeStats(120, 8, 20, 100);

        characterController = new CharacterController(this)
        {
            Owner = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            Abilities = abilities,
            CharacterStats = characterStats,
            Effects = effects,
            Shield = shield.GetComponent<MeshRenderer>()
        };
    }
}

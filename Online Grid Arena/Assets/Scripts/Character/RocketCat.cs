using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCat : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        IEffect catScratchFever = new CatScratchFever();

        IAbility catScratchFeverAbility = new CatScratchFeverAbility(this, catScratchFever);
        IAbility scratch = new Scratch(this, catScratchFeverAbility);
        IAbility blastoff = new BlastOff(this);
        IAbility kamikaze = new Kamikaze(this);

        var abilities = new List<IAbility>() { scratch, blastoff, catScratchFeverAbility, kamikaze };

        var characterStats = InitializeStats(120, 6, 25, 100);

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

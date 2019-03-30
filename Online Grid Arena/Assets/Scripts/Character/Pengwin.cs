using System.Collections.Generic;
using UnityEngine;

public sealed class Pengwin : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();

        var characterStats = InitializeStats(140, 4, 10, 100);

        var effects = new List<IEffect>() { };

        IAbility slap = new Slap(this);
        IAbility slide = new Slide(this);
        IAbility huddle = new Huddle(this);
        IAbility arcticFury = new ArcticFury(this);

        var abilities = new List<IAbility>() { slap, slide, huddle, arcticFury };

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

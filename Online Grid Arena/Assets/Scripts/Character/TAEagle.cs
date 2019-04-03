using System.Collections.Generic;
using UnityEngine;

public sealed class TAEagle : AbstractCharacter
{
    protected override void Awake()
    {
        base.Awake();
        transform.GetChild(1).transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        IAbility swoopDown = new SwoopDown(this);
        IAbility refactor = new Refactor(this);
        IAbility importDLL = new ImportDLLs(this);
        IAbility codeReview = new CodeReview(this);

        var abilities = new List<IAbility>() { swoopDown, refactor, importDLL, codeReview };
        
        var characterStats = InitializeStats(100, 5, 20, 100);

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

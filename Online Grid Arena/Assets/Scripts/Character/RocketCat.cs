using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCat : AbstractCharacter
{
    void Awake()
    {
        IEffect catScratchFever = new CatScratchFever(Resources.Load<Sprite>("Sprites/Abilities/claw-marks"));
        // TODO: Fix
        IAbility catScratchFeverAbility = new CatScratchFeverAbility(this, catScratchFever);
        IAbility scratch = new Scratch(this, catScratchFeverAbility);
        IAbility blastoff = new BlastOff(this);

        var abilities = new List<IAbility>() { scratch, blastoff, catScratchFeverAbility };
        var effects = new List<IEffect>() { };
        
   
        ICharacterStat health = new CharacterStat(120.0f);
        ICharacterStat moves = new CharacterStat(6.0f);
        ICharacterStat attack = new CharacterStat(25.0f);
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
            Effects = effects
        };
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
        EventBus.Subscribe<StartNewTurnEvent>(characterController);
        EventBus.Subscribe<EndTurnButtonEvent>(characterController);
    }


}

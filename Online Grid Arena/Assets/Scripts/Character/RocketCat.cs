using System.Collections.Generic;
using UnityEngine;

public sealed class RocketCat : AbstractCharacter
{
    void Awake()
    {
        IEffect catScratchFever = new CatScratchFever();

        IAbility catScratchFeverAbility = new CatScratchFeverAbility(this, catScratchFever);
        IAbility scratch = new Scratch(this, catScratchFeverAbility);
        IAbility blastoff = new BlastOff(this);
        IAbility placeholder = new Placeholder(this);

        var abilities = new List<IAbility>() { scratch, blastoff, catScratchFeverAbility, placeholder };
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

        ActiveCircle = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/ActiveCircle"), this.transform);
        ActiveCircle.transform.parent = this.transform;

        HealthBar = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/HealthBar"), this.transform);
        HealthBar.transform.parent = this.transform;

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

        TeamColorIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/CharColorMarker"), this.transform);
        TeamColorIndicator.transform.parent = this.transform;
        TeamColorIndicator.GetComponent<SpriteRenderer>().color = borderColor;
    }
}

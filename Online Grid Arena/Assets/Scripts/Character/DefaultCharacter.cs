using System.Collections.Generic;

public sealed class DefaultCharacter : AbstractCharacter
{
    void Awake()
    {
        // Init abilities

        IAbility defaultAttack = new DefaultAttack(this);
        IAbility defaultHeal = new DefaultHeal(this);

        var abilities = new List<IAbility>() { defaultAttack, defaultHeal };

        // Init stats

        ICharacterStat health = new CharacterStat(100.0f);
        ICharacterStat moves = new CharacterStat(5.0f);

        var characterStats = new Dictionary<string, ICharacterStat>()
        {
            {"health", health},
            {"moves", moves}
        };

        characterController = new CharacterController()
        {
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon,
            BorderColor = borderColor,
            HealthBar = GetComponentInChildren<HealthBar>(),
            Abilities = abilities,
            CharacterStats = characterStats
        };
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
    }
}

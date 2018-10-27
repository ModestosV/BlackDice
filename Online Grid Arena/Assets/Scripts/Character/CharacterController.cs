using System.Collections.Generic;

[System.Serializable]
public class CharacterController : ICharacterController
{
    public CharacterStatNameSet characterStatNameSet;
    public List<CharacterStat> characterStats;
    public int ownedByPlayer;
    public ICharacter Character { get; set; }
    public List<Ability> abilities;

    public CharacterStatNameSet CharacterStatNameSet { get { return characterStatNameSet; } }

    public List<ICharacterStat> CharacterStats
    {
        get
        {
            List<ICharacterStat> statList = new List<ICharacterStat>();
            foreach (ICharacterStat stat in characterStats)
            {
                statList.Add(stat);
            }
            return statList;
        }
    }

    public List<IAbility> Abilities
    {
        get
        {
            List<IAbility> abilityList = new List<IAbility>();
            foreach (IAbility ability in abilities)
            {
                abilityList.Add(ability);
            }
            return abilityList;
        }
    }

    public int OwnedByPlayer { get { return ownedByPlayer; } }

    public void MoveToTile(IHexTile targetTile)
    {
        Character.GetOccupiedTile().Controller.Deselect();
        Character.GetOccupiedTile().Controller.OccupantCharacter = null;

        Character.MoveToTile(targetTile);

        targetTile.Controller.OccupantCharacter = Character;
        targetTile.Controller.Select();
    }  
    
    public void Damage(float damage)
    {
        characterStats[0].AddModifier(new StatModifier(-1.0f, StatModType.Flat));
    }
}

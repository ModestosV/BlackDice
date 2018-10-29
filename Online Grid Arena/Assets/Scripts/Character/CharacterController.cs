using System;
using System.Collections.Generic;

[Serializable]
public class CharacterController : ICharacterController
{
    public int ownedByPlayer;
    public ICharacter Character { get; set; }

    public CharacterStatNameSet CharacterStatNameSet { get; set; }

    public List<ICharacterStat> CharacterStats { get; set; }
    public List<IAbility> Abilities { get; set; }

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
        CharacterStats[0].AddModifier(new StatModifier(-1.0f, StatModType.Flat));
    }
}

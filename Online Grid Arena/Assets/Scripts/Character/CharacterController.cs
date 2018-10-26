using System.Collections.Generic;

[System.Serializable]
public class CharacterController : ICharacterController
{
    public CharacterStatNameSet characterStatNameSet;
    public List<CharacterStat> characterStats;
    public int ownedByPlayer;
    public IHexTile OccupiedTile { get; set; }
    public ICharacter Character { get; set; }
    public ITurnController TurnController { get; set; }
    public int MovesRemaining { get; set; }
    public int AbilitiesRemaining { get; set; }

    public CharacterStatNameSet CharacterStatNameSet { get { return characterStatNameSet; } }

    public List<ICharacterStat> CharacterStats
    {
        get
        {
            List<ICharacterStat> stats = new List<ICharacterStat>();
            foreach (ICharacterStat stat in characterStats)
            {
                stats.Add(stat);
            }
            return stats;
        }
    }

    public int OwnedByPlayer { get { return ownedByPlayer; } }

    public void MoveToTile(IHexTile targetTile)
    {
        if (!(MovesRemaining > 0)) return;

        IHexTile currentTile = Character.Controller.OccupiedTile;
        currentTile.Controller.Deselect();
        currentTile.Controller.OccupantCharacter = null;

        Character.MoveToTile(targetTile);
        Character.Controller.OccupiedTile = targetTile;

        targetTile.Controller.OccupantCharacter = Character;
        targetTile.Controller.Select();

        MovesRemaining--;
        CheckExhausted();
    }

    public void UseAbility()
    {
        AbilitiesRemaining--;
        CheckExhausted();
    }

    private bool CheckExhausted()
    {
        if (!(MovesRemaining > 0 || AbilitiesRemaining > 0))
        {
            TurnController.EndTurn();
            return true;
        }
        return false;
    }

    public void Refresh()
    {
        MovesRemaining = 1;
        AbilitiesRemaining = 0;
    }

    public float GetInitiative()
    {
        return 1.0f;
    }
    
}

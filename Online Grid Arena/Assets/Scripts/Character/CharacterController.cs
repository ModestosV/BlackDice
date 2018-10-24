using System.Collections.Generic;

[System.Serializable]
public class CharacterController : ICharacterController
{
    public CharacterStatNameSet characterStatNameSet;
    public List<CharacterStat> characterStats;
    public int ownedByPlayer;

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
    
}

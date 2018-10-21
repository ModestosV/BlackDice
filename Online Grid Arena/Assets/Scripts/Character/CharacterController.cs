using System.Collections.Generic;

[System.Serializable]
public class CharacterController
{
    public CharacterStatNameSet characterStatNameSet;
    public List<CharacterStat> characterStats;

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
    
}

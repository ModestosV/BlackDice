using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Character Stat Name Set")]
public class CharacterStatNameSet : ScriptableObject
{
    public List<string> statNames;

    public List<string> StatNames { get { return statNames; } }
}

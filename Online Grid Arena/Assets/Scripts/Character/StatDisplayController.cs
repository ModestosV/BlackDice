using System;
using UnityEngine.UI;

[Serializable]
public class StatDisplayController {

    public Text nameText;
    public Text valueText;

    public ICharacterStat CharacterStat { get; set; }
}

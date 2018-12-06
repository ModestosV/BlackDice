using System.Collections.Generic;

public interface IStatPanelController
{
    List<IStatDisplay> StatDisplays { set; }
    Dictionary<string, ICharacterStat> CharacterStats { set; }

    void UpdateStatValues();

    void DisableStatDisplays();
    void EnableStatDisplays();
}

using System.Collections.Generic;

public interface IStatPanelController
{
    List<IStatDisplay> StatDisplays { set; }
    List<ICharacterStat> CharacterStats { set; }
    List<string> StatNames { set; }

    void UpdateStatValues();
    void UpdateStatNames();
    void DisableStatDisplays();
    void EnableStatDisplays();
}

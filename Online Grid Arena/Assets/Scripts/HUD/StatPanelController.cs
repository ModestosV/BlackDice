using System.Collections.Generic;

public class StatPanelController : IStatPanelController
{
    public List<IStatDisplay> StatDisplays { protected get; set; }
    public Dictionary<string, ICharacterStat> CharacterStats { protected get; set; }
    public List<string> StatNames { protected get; set; }
    
    public void UpdateStatValues()
    {
        StatDisplays[0].SetCurrentValueText(CharacterStats["health"].CurrentValue.ToString());
        StatDisplays[0].SetMaxValueText(CharacterStats["health"].Value.ToString());

        StatDisplays[1].SetCurrentValueText(CharacterStats["moves"].CurrentValue.ToString());
        StatDisplays[1].SetMaxValueText(CharacterStats["moves"].Value.ToString());
    }

    public void DisableStatDisplays()
    {
        foreach (IStatDisplay display in StatDisplays)
        {
            display.Deactivate();
        }
    }

    public void EnableStatDisplays()
    {
        foreach (IStatDisplay display in StatDisplays)
        {
            display.Activate();
        }
    }
}

using System.Collections.Generic;

public sealed class StatPanelController : IStatPanelController
{
    public List<IStatDisplay> StatDisplays { private get; set; }
    public Dictionary<string, ICharacterStat> CharacterStats { private get; set; }
    public List<string> StatNames { private get; set; }
    
    public void UpdateStatValues()
    {
        StatDisplays[0].SetCurrentValueText(CharacterStats["health"].CurrentValue.ToString());
        StatDisplays[0].SetMaxValueText(CharacterStats["health"].Value.ToString());

        StatDisplays[1].SetCurrentValueText(CharacterStats["moves"].CurrentValue.ToString());
        StatDisplays[1].SetMaxValueText(CharacterStats["moves"].Value.ToString());

        StatDisplays[2].SetCurrentValueText(CharacterStats["attack"].CurrentValue.ToString());
        StatDisplays[2].SetMaxValueText("");

        StatDisplays[3].SetCurrentValueText(CharacterStats["defense"].CurrentValue.ToString());
        StatDisplays[3].SetMaxValueText("");
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

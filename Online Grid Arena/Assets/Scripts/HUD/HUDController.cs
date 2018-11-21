using System;
using System.Collections.Generic;

[Serializable]
public class HUDController : IHUDController
{
    public IStatPanelController SelectedStatPanel { protected get; set; }
    public IPlayerPanel SelectedPlayerPanel { protected get; set; }

    public IStatPanelController TargetStatPanel { protected get; set; }
    public IPlayerPanel TargetPlayerPanel { protected get; set; }

    public void ClearSelectedHUD()
    {
        SelectedStatPanel.DisableStatDisplays();
        SelectedPlayerPanel.ClearPlayerName();
    }

    public void UpdateSelectedHUD(Dictionary<string, ICharacterStat> characterStats, string playerName)
    {
        SelectedStatPanel.EnableStatDisplays();
        SelectedStatPanel.CharacterStats = characterStats;
        SelectedStatPanel.UpdateStatValues();
        SelectedPlayerPanel.SetPlayerName($"Player {playerName}");
    }

    public void ClearTargetHUD()
    {
        TargetStatPanel.DisableStatDisplays();
        TargetPlayerPanel.ClearPlayerName();
    }

    public void UpdateTargetHUD(Dictionary<string, ICharacterStat> characterStats, string playerName)
    {
        TargetStatPanel.EnableStatDisplays();
        TargetStatPanel.CharacterStats = characterStats;
        TargetStatPanel.UpdateStatValues();
        TargetPlayerPanel.SetPlayerName($"Player {playerName}");
    }
}

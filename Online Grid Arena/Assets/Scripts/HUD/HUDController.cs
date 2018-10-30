using System;

[Serializable]
public class HUDController : IHUDController
{
    public IStatPanel SelectedStatPanel { get; set; }
    public IPlayerPanel SelectedPlayerPanel { get; set; }

    public IStatPanel TargetStatPanel { get; set; }
    public IPlayerPanel TargetPlayerPanel { get; set; }

    public void ClearSelectedHUD()
    {
        SelectedStatPanel.Controller.DisableStatDisplays();
        SelectedPlayerPanel.ClearPlayerName();
    }

    public void UpdateSelectedHUD(ICharacter character)
    {
        SelectedStatPanel.Controller.EnableStatDisplays();
        SelectedStatPanel.Controller.SetCharacter(character);
        SelectedStatPanel.Controller.UpdateStatNames();
        SelectedStatPanel.Controller.UpdateStatValues();
        SelectedPlayerPanel.SetPlayerName($"Player {character.Controller.OwnedByPlayer + 1}");
    }

    public void ClearTargetHUD()
    {
        TargetStatPanel.Controller.DisableStatDisplays();
        TargetPlayerPanel.ClearPlayerName();
    }

    public void UpdateTargetHUD(ICharacter character)
    {
        TargetStatPanel.Controller.EnableStatDisplays();
        TargetStatPanel.Controller.SetCharacter(character);
        TargetStatPanel.Controller.UpdateStatNames();
        TargetStatPanel.Controller.UpdateStatValues();
        TargetPlayerPanel.SetPlayerName($"Player {character.Controller.OwnedByPlayer + 1}");
    }
}

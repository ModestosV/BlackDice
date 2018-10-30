public interface IHUDController
{
    IStatPanel SelectedStatPanel { get; set; }
    IPlayerPanel SelectedPlayerPanel { get; set; }

    IStatPanel TargetStatPanel { get; set; }
    IPlayerPanel TargetPlayerPanel { get; set; }

    void ClearSelectedHUD();
    void UpdateSelectedHUD(ICharacter character);

    void ClearTargetHUD();
    void UpdateTargetHUD(ICharacter character);
}

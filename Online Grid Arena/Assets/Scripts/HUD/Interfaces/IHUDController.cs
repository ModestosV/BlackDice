using System.Collections.Generic;

public interface IHUDController
{
    void ClearSelectedHUD();
    void UpdateSelectedHUD(Dictionary<string, ICharacterStat> characterStats, string playerName);

    void ClearTargetHUD();
    void UpdateTargetHUD(Dictionary<string, ICharacterStat> characterStats, string playerName);
}

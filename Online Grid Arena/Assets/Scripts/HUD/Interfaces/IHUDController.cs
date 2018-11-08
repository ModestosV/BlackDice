using System.Collections.Generic;

public interface IHUDController
{
    void ClearSelectedHUD();
    void UpdateSelectedHUD(List<string> statNames, List<ICharacterStat> characterStats, string playerName);

    void ClearTargetHUD();
    void UpdateTargetHUD(List<string> statNames, List<ICharacterStat> characterStats, string playerName);
}

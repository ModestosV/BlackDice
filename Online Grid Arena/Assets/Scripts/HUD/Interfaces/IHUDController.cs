using System.Collections.Generic;

public interface IHUDController
{
    void ClearSelectedHUD();
    void UpdateSelectedHUD(Dictionary<string, ICharacterStat> characterStats, string playerName, List<IAbility> abilities);

    void ClearTargetHUD();
    void UpdateTargetHUD(Dictionary<string, ICharacterStat> characterStats, string playerName);
}

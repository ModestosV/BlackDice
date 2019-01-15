using UnityEngine;
using System.Collections.Generic;

public interface IAbility
{
    Sprite AbilityIcon { get; set; }
    List<IEffect> Effects { get; set; }

    void Execute(List<IHexTileController> targetTile);
    void AddEffect(IEffect effect);
}

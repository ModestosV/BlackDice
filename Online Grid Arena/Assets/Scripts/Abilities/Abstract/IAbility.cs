using UnityEngine;
using System.Collections.Generic;

public interface IAbility
{
    Sprite AbilityIcon { get; }
    List<IEffect> Effects { get; }
    string Description { get; }

    void Execute(List<IHexTileController> targetTile);
    void AddEffect(IEffect effect);
}

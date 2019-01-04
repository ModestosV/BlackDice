using System.Collections.Generic;

public interface IEffect
{
    EffectType Type { get; set; }
    void Apply(IHexTileController targetTile);
    bool HasRunOut();
    void Refresh();
    void Decrement();
    Dictionary<string, float> GetEffects();
    bool RemoveEffect();
}
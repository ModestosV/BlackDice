public interface IEffect
{
    EffectType Type { get; set; }
    void Apply(IHexTileController targetTile);
    bool HasRunOut();
    void Refresh();
}
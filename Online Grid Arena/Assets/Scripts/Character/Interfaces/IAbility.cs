public interface IAbility
{
    AbilityType Type { get; set; }
    void Execute(IHexTileController targetTile);
}

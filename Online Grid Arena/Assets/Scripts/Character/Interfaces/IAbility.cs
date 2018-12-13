public interface IAbility
{
    AbilityType Type { get; set; }
    void Execute(IHexTileController targetTile);
    bool IsInRange(int range);
    bool IsOnCooldown();
    void Refresh();
}

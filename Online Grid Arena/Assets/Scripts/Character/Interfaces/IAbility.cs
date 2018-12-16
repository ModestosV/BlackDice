public interface IAbility
{
    AbilityType AbilityType { get; set; }
    void Execute(IHexTileController targetTile);
    bool IsInRange(int range);
    bool IsOnCooldown();
    void Refresh();
}

public interface IAbility
{
    AbilityType Type { get; set; }
    void Execute(IHexTileController targetTile);
    bool IsOnCooldown();
    void Refresh();
    void ModifyPower(float amount);
}

public sealed class BasicHealAbility : Ability
{
    public override void Execute(IHexTileController targetTile)
    {
        float healingToApply = Values["power"];
        targetTile.OccupantCharacter.Heal(healingToApply);
    }
}

public sealed class BasicAttackAbility : Ability
{
    public override void Execute(IHexTileController targetTile)
    {
        float damageToDeal = Values["power"];
        targetTile.OccupantCharacter.Damage(damageToDeal);
    }
}

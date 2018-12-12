using System.Collections.Generic;

public sealed class BasicAttackAbility : Ability
{
    public BasicAttackAbility(float power, float range)
    {
        Type = AbilityType.TARGET_ENEMY;
        Values = new Dictionary<string, float>() {
            {"power", power },
            {"range", range }
        };
    }

    public override void Execute(IHexTileController targetTile)
    {
        float damageToDeal = Values["power"];
        targetTile.OccupantCharacter.Damage(damageToDeal);
    }
}

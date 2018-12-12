using System.Collections.Generic;

public sealed class BasicHealAbility : Ability
{
    public BasicHealAbility(float power, float range)
    {
        Type = AbilityType.TARGET_ALLY;
        Values = new Dictionary<string, float>() {
            {"power", power },
            {"range", range }
        };
    }
    public override void Execute(IHexTileController targetTile)
    {
        float healingToApply = Values["power"];
        targetTile.OccupantCharacter.Heal(healingToApply);
    }
}

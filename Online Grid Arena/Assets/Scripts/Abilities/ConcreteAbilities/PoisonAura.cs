using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class PoisonAura : AbstractPassiveAbility
{
    public PoisonAura(AgentFrog character, IEffect effect) : base(
        Resources.Load<Sprite>("Sprites/Abilities/claw-marks"), character,
        "Poison Aura - Passive \nA poison aura around Agent Frog damages all foes in the range at the start of their turn and at the end of his.")
    {
        AddEffect(effect);
    }

    protected override void PrimaryAction(List<IHexTileController> targetTile)
    {
        character.Controller.ApplyEffect(this.Effects[0]);
    }

}

using System.Collections.Generic;
using UnityEngine;

public class Shear : AbstractActiveAbility
{
    private IEffect woolArmor;
    public Shear(ICharacter character) : base(
        Resources.Load<Sprite>("Sprites/Abilities/huddle"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/DefenseBuffAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/huddle"),
        character,
        4,
        "Shear - Special Ability \nSheepadin shears his armor to heal himself and adjacent allies for 20*(number of stacks consumed). Consumes half of his Wool Armor stacks.")
    {
        woolArmor = new WoolArmorEffect();
    }

    protected override void PrimaryAction(List<IHexTileController> targetTiles)
    {
        //consume half stacks
        int stacksToConsume = this.character.Controller.Effects.Count / 2;

        targetTiles[0].Heal(stacksToConsume*20);
        foreach (IHexTileController neighbor in targetTiles[0].GetNeighbors())
        {
            neighbor.Heal(20*stacksToConsume);
        }
        PlaySoundEffect();

        for (int i = 0; i < stacksToConsume; i++)
        {
            this.character.Controller.ConsumeOneStack(woolArmor);
        }
    }
}
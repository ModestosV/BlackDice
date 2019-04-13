using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class ArcticFury : TargetedAbility, IEventSubscriber
{
    private bool hasSomeoneDied = false;
    private const int ROUNDS = 3;

    public ArcticFury(ICharacter activeCharacter) : base(
        Resources.Load<Sprite>("Sprites/Abilities/arctic-fury"),
        Resources.Load<GameObject>("Prefabs/AbilityAnimations/SlapAnimation"),
        Resources.Load<AudioClip>("Audio/Ability/slap"),
        activeCharacter,
        6,
        1,
        AbilityType.TARGET_ENEMY,
        "Slap - Ultimate \nPengwin challenges an adjacent enemy to a duel. Both characters take turns performing their Q on each other," +
                "with Pengwin going first. Lasts until a character dies or 3 attacks are performed.")
    { }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(DeathEvent))
        {
            hasSomeoneDied = true;
        }
    }

    protected override async void PrimaryAction(List<IHexTileController> targetTiles)
    {
        hasSomeoneDied = false;

        var isPengwinsTurn = true;
        var enemy = targetTiles[0].OccupantCharacter;
        var enemyQ = targetTiles[0].OccupantCharacter.Abilities[0];
        var slap = character.Controller.Abilities[0];

        for (int i = 0; i < ROUNDS * 2; i++)
        {
            if (!hasSomeoneDied)
            {
                if(isPengwinsTurn)
                {
                    slap.Execute(targetTiles);
                }
                else
                {
                    enemyQ.Execute(new List<IHexTileController>() { character.Controller.OccupiedTile });
                }
                isPengwinsTurn = !isPengwinsTurn;
                await Task.Delay(1200);
            }
            else
            {
                Debug.Log("Duel terminated as a character has died.");
                break;
            }
        }
    }
}

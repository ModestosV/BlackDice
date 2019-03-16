using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

public class Stage3Controller : IEventSubscriber
{
    private readonly List<ICharacterController> characters;
    private readonly ArrowIndicator[] arrows;
    private readonly List<IPlayer> players;
    private bool finalStep;
    private readonly int indexCat;
    private readonly int indexPengwin;
    private readonly int indexScratch;
    private readonly int indexSlide;
    private readonly int indexBoost;

    private const String TEXT_STEP_2 = "Press or click Q to attack";
    private const String TEXT_STEP_3 = "Select Pengwin (CLICK)";
    private const String TEXT_STEP_4 = "Press or click W to use ability\n(in a straight line)";
    private const String TEXT_STEP_5 = "Select Rocket Cat again";
    private const String TEXT_STEP_6 = "Press or click W to use ability";
    private const String TEXT_COMPLETED = "Congratulations! Stage Completed";

    public Stage3Controller(List<ICharacterController> characters, ArrowIndicator[] arrows, List<IPlayer> players)
    {
        this.characters = characters;
        this.arrows = arrows;
        this.players = players;
        this.finalStep = false;

        foreach (ICharacterController c in characters)
        {
            if(c.Character.GetType() == typeof(RocketCat))
            {
                indexCat = characters.IndexOf(c);
                foreach(IAbility ab in c.Abilities)
                {
                    if (ab.GetType() == typeof(Scratch))
                    {
                        indexScratch = c.Abilities.IndexOf(ab);
                    }
                    if(ab.GetType() == typeof(BlastOff))
                    {
                        indexBoost = c.Abilities.IndexOf(ab);
                    }
                }
            }
            else
            {
                indexPengwin = characters.IndexOf(c);
                foreach (IAbility ab in c.Abilities)
                {
                    if (ab.GetType() == typeof(Slide))
                    {
                        indexSlide = c.Abilities.IndexOf(ab);
                    }
                }
            }
        }
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();     

        if (type == typeof(StartNewTurnEvent))
        {
            if (!characters[indexCat].Effects.Any() && characters[indexCat].CharacterState != CharacterState.EXHAUSTED)
            {
                var arrow = arrows.Select(x => x.GameObject.tag == "CatArrow" ? x : null).ToList();

                foreach (ArrowIndicator obj in arrow)
                {
                    if (obj != null)
                    {
                        obj.ShowArrow();
                    }
                }
            }
            else
            {
                var arrow = arrows.Select(x => x.GameObject.tag == "PengwinArrow" ? x : null).ToList();

                foreach (ArrowIndicator obj in arrow)
                {
                    if (obj != null)
                    {
                        obj.ShowArrow();
                    }
                }
            }
        }
        else if (type == typeof(SelectCharacterEvent))
        {
            if (!characters[indexCat].Effects.Any())
            {
                var active = (SelectCharacterEvent)@event;
                if (active.Character.Character.GetType() == typeof(RocketCat) && !active.Character.CheckAbilitiesExhausted() && characters[indexPengwin].CharacterState != CharacterState.EXHAUSTED && !finalStep)
                {
                    foreach (ArrowIndicator arrow in arrows)
                    {
                        if (arrow.GameObject.tag == "TutorialArrow")
                        {
                            arrow.ShowArrow();
                        }
                        else if (arrow.GameObject.tag == "CatArrow")
                        {
                            arrow.HideArrow();
                        }
                    }
                }
                else if ((active.Character.Character.GetType() == typeof(RocketCat) && !active.Character.CheckAbilitiesExhausted() && characters[indexPengwin].CharacterState == CharacterState.EXHAUSTED) || finalStep)
                {
                    foreach (ArrowIndicator arrow in arrows)
                    {
                        if (arrow.GameObject.tag == "TutorialArrowW")
                        {
                            arrow.ShowArrow();
                        }
                        else if (arrow.GameObject.tag == "CatArrow")
                        {
                            arrow.HideArrow();
                        }
                    }
                }
            }
            else
            {
                var active = (SelectCharacterEvent)@event;
                if (active.Character.Character.GetType() == typeof(Pengwin) && !active.Character.CheckAbilitiesExhausted())
                {
                    foreach (ArrowIndicator arrow in arrows)
                    {
                        if (arrow.GameObject.tag == "TutorialArrowW")
                        {
                            arrow.ShowArrow();
                        }
                        else if (arrow.GameObject.tag == "PengwinArrow")
                        {
                            arrow.HideArrow();
                        }
                    }
                }
            }
        }
        else if (type == typeof(AbilityClickEvent) || type == typeof(AbilitySelectedEvent))
        {
            int abilityIndex = 0;
            if(type == typeof(AbilityClickEvent))
            {
                var ability = (AbilityClickEvent)@event;
                abilityIndex = ability.AbilityIndex;
            }
            else
            {
                var ability = (AbilitySelectedEvent)@event;
                abilityIndex = ability.AbilityIndex;
            }

            if (abilityIndex != indexScratch && !characters[indexPengwin].IsActive)
            {
                foreach (ArrowIndicator arrow in arrows)
                {
                    if (arrow.GameObject.tag == "CatArrow")
                    {
                        arrow.HideArrow();
                    }
                    else if (arrow.GameObject.tag == "TutorialArrowW")
                    {
                        arrow.ShowArrow();
                    }
                }
            }
            else
            {
                foreach (ArrowIndicator arrow in arrows)
                {
                    if (arrow.GameObject.tag == "TutorialArrow")
                    {
                        arrow.HideArrow();
                    }
                    else if (arrow.GameObject.tag == "PengwinArrow")
                    {
                        arrow.ShowArrow();
                    }
                }
            }

        }
        else if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectMode = (UpdateSelectionModeEvent)@event;

            if (selectMode.SelectionMode.Equals(SelectionMode.FREE) && characters[indexCat].Effects.Any() && characters[indexCat].CharacterState != CharacterState.EXHAUSTED)
            {
                characters[indexCat].HUDController.ClearSelectedHUD();
                characters[indexCat].HUDController.ClearTargetHUD();
                EventBus.Publish(new DeselectSelectedTileEvent());
                characters[indexCat].EndOfTurn();
                EventBus.Publish(new StartNewTurnEvent());
            }
            else if (selectMode.SelectionMode.Equals(SelectionMode.ABILITY) && characters[indexCat].CharacterState == CharacterState.EXHAUSTED)
            {
                foreach (ArrowIndicator arrow in arrows)
                {
                    if (arrow.GameObject.tag == "TutorialArrowW")
                    {
                        arrow.HideArrow();
                    }
                    else if (arrow.GameObject.tag == "PengwinArrow")
                    {
                        arrow.ShowArrow();
                    }
                }
            }
            else if (selectMode.SelectionMode.Equals(SelectionMode.FREE) && characters[indexPengwin].IsActive && characters[indexCat].CharacterState == CharacterState.EXHAUSTED)
            {
                characters[indexPengwin].HUDController.ClearSelectedHUD();
                characters[indexPengwin].HUDController.ClearTargetHUD();
                characters[indexPengwin].EndOfTurn();
                EventBus.Publish(new DeselectSelectedTileEvent());
                EventBus.Publish(new NewRoundEvent(characters[indexPengwin]));
                EventBus.Publish(new NewRoundEvent(characters[indexCat]));

                EventBus.Publish(new StartNewTurnEvent());
                EventBus.Publish(new StartNewTurnEvent());

                this.finalStep = true;

                foreach (ArrowIndicator arrow in arrows)
                {
                    if (arrow.GameObject.tag == "CatArrow")
                    {
                        arrow.ShowArrow();
                    }
                    else
                    {
                        arrow.HideArrow();
                    }
                }
            }
            else if (selectMode.SelectionMode.Equals(SelectionMode.FREE) && characters[indexCat].IsActive && !characters[indexCat].CanUseAbility(indexBoost) && finalStep)
            {
                foreach (ArrowIndicator arrow in arrows)
                {
                   arrow.HideArrow();
                }

                characters[indexCat].HUDController.ClearSelectedHUD();
                characters[indexCat].HUDController.ClearTargetHUD();
                characters[indexCat].EndOfTurn();
                EventBus.Publish(new DeselectSelectedTileEvent());
                EventBus.Publish(new StartNewTurnEvent());
                EventBus.Publish(new StageCompletedEvent(1));
            }
        }
    }
}
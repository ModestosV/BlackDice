using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class Stage3Controller : IEventSubscriber
{
    private List<ICharacterController> characters;
    private ArrowIndicator[] arrows;

    public Stage3Controller(List<ICharacterController> characters, ArrowIndicator[] arrows)
    {
        this.characters = characters;
        this.arrows = arrows;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        var indexCat = 0;
        var indexPengwin = 0;
        

        foreach (ICharacterController c in characters)
        {
            if(c.Character.GetType() == typeof(RocketCat))
            {
                indexCat = characters.IndexOf(c);
            }
            else
            {
                indexPengwin = characters.IndexOf(c);
            }
        }

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
                if (active.Character.Character.GetType() == typeof(RocketCat) && !active.Character.CheckAbilitiesExhausted())
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
            foreach (ArrowIndicator arrow in arrows)
            {
                if(arrow.GameObject.tag == "TutorialArrow")
                {
                    arrow.HideArrow();
                }
                else if (arrow.GameObject.tag == "PengwinArrow")
                {
                    arrow.ShowArrow();
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
        }
    }
}
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

        if (type == typeof(UpdateSelectionModeEvent))
        {
            var modeOfSelection = (UpdateSelectionModeEvent)@event;

            if (modeOfSelection.SelectionMode.Equals(SelectionMode.FREE))
            {
                foreach(CharacterController controller in this.characters)
                {
                    if (controller.Character.GetType() == typeof(RocketCat) && controller.CheckAbilitiesExhausted())
                    {
                        foreach (IEffect effect in controller.Effects)
                        {
                            if (effect.GetType() == typeof(CatScratchFever))
                            {
                                Debug.Log(ToString() + " Starting new tutorial step");
                                controller.ClearSelectedHUD();
                                controller.ClearTargetHUD();
                                controller.EndOfTurn();
                            }
                        }
                    }
                }
            }
        }
        else if (type == typeof(StartNewTurnEvent))
        {
            foreach (ArrowIndicator arrow in arrows)
            {
                if (arrow.GameObject.tag == "CatArrow")
                {
                    arrow.ShowArrow();
                }

            }
        }
        else if (type == typeof(ActiveCharacterEvent))
        {
            foreach (ArrowIndicator arrow in arrows)
            {
                if (arrow.GameObject.tag == "CatArrow")
                {
                    arrow.HideArrow();
                }
                if (arrow.GameObject.tag == "TutorialArrow")
                {
                    arrow.ShowArrow();
                }
            }
        }
        else if (type == typeof(AbilitySelectedEvent) || type == typeof(AbilityClickEvent))
        {
            foreach (ArrowIndicator arrow in arrows)
            {
                if (arrow.GameObject.tag == "TutorialArrow")
                {
                    arrow.HideArrow();
                }
                if (arrow.GameObject.tag == "PengwinArrow")
                {
                    arrow.ShowArrow();
                }
            }
        }
        else if (type == typeof(ExhaustCharacterEvent))
        {
            foreach (ArrowIndicator arrow in arrows)
            {
                if (arrow.GameObject.tag == "CatArrow")
                {
                    arrow.ShowArrow();
                }
                if (arrow.GameObject.tag == "PengwinArrow")
                {
                    arrow.HideArrow();
                }
            }
        }

    }
}
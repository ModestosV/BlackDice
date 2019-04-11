using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;

public class Stage6Controller : AbstractStageController,IEventSubscriber
{
    private readonly List<ICharacterController> characters;
    private readonly ArrowIndicator[] arrows;
    private bool stageFailedFlag;
    private readonly int indexCat;
    private readonly int indexPengwin;
    private readonly int indexScratch;
    private readonly int indexSlide;
    private readonly int indexBoost;

    private const string TEXT_STEP_2 = "Press or click E to attack Agent Frog";
    private const string TEXT_STEP_3 = "Select TAEagle (CLICK)";
    private const string TEXT_STEP_4 = "Press or click E to use ability\n(on sheepadin)";
    private const string STAGE_FAILED = "Stage Failed!\nRedirecting Tutorial";

    private readonly List<Action> stepMethods = new List<Action>();
    private const int STAGE_INDEX = 6;
    private int currentStep;
    private int abilitySelected;
    private Type type;
    private IEvent eventHandled;

    public Stage6Controller(List<ICharacterController> characters, ArrowIndicator[] arrows, List<IPlayer> players)
    {
        this.characters = characters;
        this.arrows = arrows;
        this.stageFailedFlag = false;

        foreach (ICharacterController c in characters)
        {
            if (c.Character.GetType() == typeof(RocketCat))
            {
                indexCat = characters.IndexOf(c);
                foreach (IAbility ab in c.Abilities)
                {
                    if (ab.GetType() == typeof(Scratch))
                    {
                        indexScratch = c.Abilities.IndexOf(ab);
                    }
                    if (ab.GetType() == typeof(BlastOff))
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

        currentStep = 0;
        stepMethods.Add(this.HandleStep1);
        stepMethods.Add(this.HandleStep2);
        stepMethods.Add(this.HandleStep3);
        stepMethods.Add(this.HandleStep4);
        stepMethods.Add(this.HandleStep4Continued);
        stepMethods.Add(() => CompleteStage(STAGE_INDEX));
    }

    private void HandleStep1()
    {
        var arrow = arrows.Select(x => x.GameObject.CompareTag("BeeArrow") ? x : null).ToList();

        foreach (ArrowIndicator obj in arrow)
        {
            if (obj != null)
            {
                obj.Show();
            }
            else
            {
                obj.Hide();
            }
        }
    }

    private void HandleStep2()
    {

    }

    private new void CompleteStage(int StageIndex)
    {
        foreach (ArrowIndicator arrow in arrows)
        {
            arrow.Hide();
        }

        characters[indexCat].HUDController.ClearSelectedHUD();
        characters[indexCat].HUDController.ClearTargetHUD();
        characters[indexCat].EndOfTurn();
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_COMPLETE;
        EventBus.Publish(new DeselectSelectedTileEvent());
        EventBus.Publish(new StartNewTurnEvent());
        EventBus.Publish(new StageCompletedEvent(StageIndex));
    }

    private void StageFailed()
    {
        stageFailedFlag = true;
        foreach (ArrowIndicator arrow in arrows)
        {
            arrow.Hide();
        }
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_FAILED;
        EventBus.Publish(new SurrenderEvent());
    }

    private void Execute()
    {
        if(!stageFailedFlag)
        {
            var localCurrent = currentStep;
            currentStep++;
            stepMethods[localCurrent].Invoke();
        }
    }

    private bool CheckCorrectInput()
    {

        if (type == typeof(StartNewTurnEvent))
        {
            List<int> validNums = new List<int>() { 0, 3, 6, 8 };
            if (!validNums.Contains(currentStep))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectMode = (UpdateSelectionModeEvent)eventHandled;

            if (selectMode.SelectionMode.Equals(SelectionMode.MOVEMENT))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (type == typeof(AbilitySelectedEvent) || type == typeof(AbilityClickEvent))
        {
            int abilityIndex = 2;
            List<int> validIndex = new List<int>() { 0, 1 };
            if (type == typeof(AbilityClickEvent))
            {
                var ability = (AbilityClickEvent)eventHandled;
                abilityIndex = ability.AbilityIndex;
            }
            else if(type == typeof(AbilitySelectedEvent))
            {
                var ability = (AbilitySelectedEvent)eventHandled;
                abilityIndex = ability.AbilityIndex;
            }

            if (!validIndex.Contains(abilityIndex))
            {
                return false;
            }
            else
            {
                if(abilityIndex == 0 && currentStep <= 2)
                {
                    return true;
                }
                else if(abilityIndex == 1 && currentStep > 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        return true;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(AbilitySelectedEvent))
        {
            var abilitySelectedEvent = (AbilitySelectedEvent)@event;

            abilitySelected = abilitySelectedEvent.AbilityIndex;
        }
        else if (type == typeof(AbilityClickEvent))
        {
            var abilitySelectedEvent = (AbilityClickEvent)@event;

            abilitySelected = abilitySelectedEvent.AbilityIndex;
        }

        stepMethods[currentStep].Invoke();

        abilitySelected = -1;

        Debug.Log(currentStep);
    }
}
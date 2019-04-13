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
    private readonly int indexBee;
    private readonly int indexEagle;
    private readonly int indexSilence;
    private readonly int indexDLL;

    private const string TEXT_STEP_2 = "Press or click E";
    private const string TEXT_STEP_3 = "Attack Agent Frog";
    private const string TEXT_STEP_4 = "Select TAEagle (CLICK)";
    private const string TEXT_STEP_5 = "Press or click";
    private const string TEXT_STEP_6 = "Select TAEagle or Agent Frog (CLICK)";
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
            if(c.Character.GetType() == typeof(RigBeeStinger))
            {
                indexBee = characters.IndexOf(c);
                foreach (IAbility ab in c.Abilities)
                {
                    if (ab.GetType() == typeof(Silence))
                    {
                        indexSilence = c.Abilities.IndexOf(ab);
                    }
                }
            }
            else if(c.Character.GetType() == typeof(TAEagle))
            {
                indexEagle = characters.IndexOf(c);
                foreach (IAbility ab in c.Abilities)
                {
                    if (ab.GetType() == typeof(ImportDLLs))
                    {
                        indexDLL = c.Abilities.IndexOf(ab);
                    }
                }
            }
        }

        currentStep = 0;
        stepMethods.Add(this.HandleStep1);
        stepMethods.Add(this.HandleStep2);
        stepMethods.Add(this.HandleStep3);
        stepMethods.Add(this.HandleStep4);
        stepMethods.Add(this.HandleStep5);
        stepMethods.Add(this.HandleStep6);
        stepMethods.Add(() => CompleteStage(STAGE_INDEX));
    }

    private void HandleStep1()
    {
        if(currentStep == 0 && type == typeof(StartNewTurnEvent))
        {
            var arrow = arrows.Select(x => x.GameObject.CompareTag("BeeArrow") ? x : null).ToList();

            foreach (ArrowIndicator obj in arrow)
            {
                if (obj != null)
                {
                    obj.Show();
                }
            }

            currentStep++;
        }
    }

    private void HandleStep2()
    {
        if (type == typeof(SelectCharacterEvent))
        {
            if(currentStep == 1 && characters[indexBee].CanUseAbility(indexSilence))
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_2;

                foreach (ArrowIndicator obj in arrows)
                {
                    if (obj.GameObject.CompareTag("TutorialArrow"))
                    {
                        obj.Show();
                    }
                    else
                    {
                        obj.Hide();
                    }
                }
                currentStep++;

            }
            else
            {
                stageFailedFlag = true;
                StageFailed();
            }
        }
    }

    private void HandleStep3()
    {
        if(type == typeof(AbilityClickEvent) || type == typeof(AbilitySelectedEvent))
        {
            if(currentStep == 2 && characters[indexBee].CanUseAbility(indexSilence))
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_3;

                foreach (ArrowIndicator obj in arrows)
                {
                    if (obj.GameObject.CompareTag("FrogArrow"))
                    {
                        obj.Show();
                    }
                    else
                    {
                        obj.Hide();
                    }
                }
                currentStep++;
            }
            else
            {
                stageFailedFlag = true;
                StageFailed();
            }
        }
    }

    private void HandleStep4()
    {
        if(type == typeof(UpdateSelectionModeEvent))
        {
            var selectionMode = (UpdateSelectionModeEvent)eventHandled;

            if (currentStep == 3 && !characters[indexBee].CanUseAbility(indexSilence) && selectionMode.SelectionMode.Equals(SelectionMode.FREE))
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_4;

                foreach (ArrowIndicator obj in arrows)
                {
                    if (obj.GameObject.CompareTag("EagleArrow"))
                    {
                        obj.Show();
                    }
                    else
                    {
                        obj.Hide();
                    }
                }
                currentStep++;
                EventBus.Publish(new DeselectSelectedTileEvent());
                EventBus.Publish(new StartNewTurnEvent());
            }
            else
            {
                stageFailedFlag = true;
                StageFailed();
            }
        }
    }

    public void HandleStep5()
    {
        if(type == typeof(SelectCharacterEvent))
        {
            if(currentStep == 4)
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_5;

                foreach (ArrowIndicator obj in arrows)
                {
                    if (obj.GameObject.CompareTag("TutorialArrow"))
                    {
                        obj.Show();
                    }
                    else
                    {
                        obj.Hide();
                    }
                }
                currentStep++;
            }
        }
    }

    private void HandleStep6()
    {
        if (type == typeof(AbilityUsedEvent))
        {
            if (currentStep == 5 && !characters[indexEagle].CanUseAbility(indexDLL))
            {
                GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_6;

                foreach (ArrowIndicator obj in arrows)
                {
                    if (obj.GameObject.CompareTag("FrogArrow") || obj.GameObject.CompareTag("EagleArrow"))
                    {
                        obj.Show();
                    }
                    else
                    {
                        obj.Hide();
                    }
                }
                currentStep++;
                EventBus.Publish(new DeselectSelectedTileEvent());
                EventBus.Publish(new StartNewTurnEvent());
            }
            else
            {
                stageFailedFlag = true;
                StageFailed();
            }
        }
    }

    private new void CompleteStage(int StageIndex)
    {
        if(currentStep <= 6) 
        {
            foreach (ArrowIndicator arrow in arrows)
            {
                arrow.Hide();
            }
            currentStep++;

            GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = STAGE_COMPLETE;
            EventBus.Publish(new DeselectSelectedTileEvent());
            EventBus.Publish(new StartNewTurnEvent());
            EventBus.Publish(new StageCompletedEvent(StageIndex));
        }
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

    public void Handle(IEvent @event)
    {
        type = @event.GetType();
        eventHandled = @event;

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

        if (!stageFailedFlag && currentStep < stepMethods.Capacity - 1)
        {
            stepMethods[currentStep].Invoke();
        }

        abilitySelected = -1;

        Debug.Log(currentStep);
    }
}
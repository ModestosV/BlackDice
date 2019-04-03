using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;

public class Stage3Controller : AbstractStageController,IEventSubscriber
{
    private readonly List<ICharacterController> characters;
    private readonly ArrowIndicator[] arrows;
    private readonly List<IPlayer> players;
    private bool stageFailedFlag;
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
    private const string STAGE_FAILED = "Stage Failed!\nRedirecting Tutorial";

    private List<Action> stepMethods = new List<Action>();
    private const int STAGE_INDEX = 3;
    private int currentStep;
    private Type type;
    private IEvent eventHandled;

    public Stage3Controller(List<ICharacterController> characters, ArrowIndicator[] arrows, List<IPlayer> players)
    {
        this.characters = characters;
        this.arrows = arrows;
        this.players = players;
        this.stageFailedFlag = false;

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

        currentStep = 0;
        stepMethods.Add(() => this.HandleStep1());
        stepMethods.Add(() => this.HandleStep2());
        stepMethods.Add(() => this.HandleStep3());
        stepMethods.Add(() => this.HandleStep4());
        stepMethods.Add(() => this.HandleStep4Continued());
        stepMethods.Add(() => this.HandleStep5());
        stepMethods.Add(() => this.HandleStep6());
        stepMethods.Add(() => CompleteStage(STAGE_INDEX));
    }

    public void HandleStep1()
    {
        var arrow = arrows.Select(x => x.GameObject.tag == "CatArrow" ? x : null).ToList();

        foreach (ArrowIndicator obj in arrow)
        {
            if (obj != null)
            {
                obj.Show();
            }
        }
    }

    public void HandleStep2()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_2;
        foreach (ArrowIndicator arrow in arrows)
        {
            if (arrow.GameObject.tag == "TutorialArrow")
            {
                arrow.Show();
            }
            else if (arrow.GameObject.tag == "CatArrow")
            {
                arrow.Hide();
            }
        }
    }

    public void HandleStep3()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_3;

        foreach (ArrowIndicator obj in arrows)
        {
            if (obj.tag == "PengwinArrow")
            {
                obj.Show();
            }
            else
            {
                obj.Hide();
            }
        }

        characters[indexCat].HUDController.ClearSelectedHUD();
        characters[indexCat].HUDController.ClearTargetHUD();
        EventBus.Publish(new DeselectSelectedTileEvent());
        EventBus.Publish(new StartNewTurnEvent());
        characters[indexCat].EndOfTurn();
    }

    public void HandleStep4()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_4;
        foreach (ArrowIndicator arrow in arrows)
        {
            if (arrow.GameObject.tag == "TutorialArrowW")
            {
                arrow.Show();
            }
            else if (arrow.GameObject.tag == "PengwinArrow")
            {
                arrow.Hide();
            }
        }
    }

    public void HandleStep5()
    {
        characters[indexPengwin].HUDController.ClearSelectedHUD();
        characters[indexPengwin].HUDController.ClearTargetHUD();
        characters[indexPengwin].EndOfTurn();
        EventBus.Publish(new DeselectSelectedTileEvent());
        EventBus.Publish(new NewRoundEvent(characters[indexPengwin]));
        EventBus.Publish(new NewRoundEvent(characters[indexCat]));

        EventBus.Publish(new StartNewTurnEvent());

        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_5;

        foreach (ArrowIndicator arrow in arrows)
        {
            if (arrow.GameObject.tag == "CatArrow")
            {
                arrow.Show();
            }
            else
            {
                arrow.Hide();
            }
        }
    }

    public void HandleStep6()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_6;
        foreach (ArrowIndicator arrow in arrows)
        {
            if (arrow.GameObject.tag == "TutorialArrowW")
            {
                arrow.Show();
            }
            else if (arrow.GameObject.tag == "CatArrow")
            {
                arrow.Hide();
            }
        }
    }

    public void HandleStep4Continued()
    {
        GameObject.FindWithTag("TutorialTooltip").GetComponent<TextMeshProUGUI>().text = TEXT_STEP_4;
        foreach (ArrowIndicator arrow in arrows)
        {
            if (arrow.GameObject.tag == "TutorialArrowW")
            {
                arrow.Hide();
            }
            else if (arrow.GameObject.tag == "PengwinArrow")
            {
                arrow.Show();
            }
        }
    }

    public new void CompleteStage(int StageIndex)
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

    public void Execute()
    {
        if(!stageFailedFlag)
        {
            var localCurrent = currentStep;
            currentStep++;
            stepMethods[localCurrent].Invoke();
        }
    }

    public bool CheckCorrectInput()
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
        type = @event.GetType();
        eventHandled = @event;

        if(!CheckCorrectInput())
        {
            StageFailed();
        }
        else if (type == typeof(StartNewTurnEvent))
        {
            if (currentStep == 0)
            {
                //first step
                Execute();
            }
        }
        else if (type == typeof(SelectCharacterEvent))
        {
            var active = (SelectCharacterEvent)@event;
            if (currentStep == 1)
            {
                //second step
                Execute();
            }
            else if (currentStep == 6)
            {
                //sixth step
                Execute();
            }
            else if (currentStep == 3)
            {
                //fourth step
                Execute();
            }
        }
        else if (type == typeof(UpdateSelectionModeEvent))
        {
            var selectMode = (UpdateSelectionModeEvent)@event;

            if (currentStep == 2 && selectMode.SelectionMode.Equals(SelectionMode.FREE))
            {
                //third step
                Execute();
            }
            else if (currentStep == 4 && selectMode.SelectionMode.Equals(SelectionMode.ABILITY))
            {
                //Fourth Step continued
                Execute();
            }
            else if (currentStep == 5 && selectMode.SelectionMode.Equals(SelectionMode.FREE))
            {
                //Fifth Step
                Execute();
            }
            else if (currentStep == 7 && selectMode.SelectionMode.Equals(SelectionMode.FREE))
            {
                //Completion
                Execute();
            }
        }
    }
}
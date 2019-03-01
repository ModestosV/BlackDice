using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class TutorialMenu: HideableUI, IEventSubscriber
{
    public string filepath = "";
    public List<Button> StageButtons;
    public int stagesCompleted = 0;

    void Start()
    {
        filepath = Application.persistentDataPath + "/playerInfo.dat";
        EventBus.Subscribe<StageCompletedEvent>(this);

        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.tag.Equals("StageButton"))
            {
                StageButtons.Add(button);
            }
        }

        VerifySaveFileExist();
    }

    public void LoadTutorialMenu()
    {
        stagesCompleted = ReadStageCompleted();

        for (int i = 0; i < stagesCompleted; i++)
        {
            StageButtons[i].interactable = true;
            StageButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        }

        if (stagesCompleted < 7)
        {
            StageButtons[stagesCompleted].interactable = true;
            StageButtons[stagesCompleted].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
        }

        Show();
    }

    public void PlayTutorialStage(int stageIndex)
    {
        SceneManager.LoadScene(stageIndex);
    }

    public void VerifySaveFileExist()
    {
        if (!File.Exists(filepath))
        {
            TutorialSavedObject tutorialSavedObject = new TutorialSavedObject() { StageCompleted = stagesCompleted };
            BinarySerialization.WriteToBinaryFile<TutorialSavedObject>(filepath, tutorialSavedObject);
        }
    }

    public void SaveStageCompleted()
    {
        TutorialSavedObject tutorialSavedObject = new TutorialSavedObject() { StageCompleted = stagesCompleted };
        BinarySerialization.WriteToBinaryFile<TutorialSavedObject>(filepath, tutorialSavedObject);
    }

    public int ReadStageCompleted()
    {
        TutorialSavedObject tutorialSavedObject = BinarySerialization.ReadFromBinaryFile<TutorialSavedObject>(filepath);
        return tutorialSavedObject.StageCompleted;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(StageCompletedEvent))
        {
            var stageCompleted = (StageCompletedEvent)@event;

            if (stageCompleted.StageIndex > stagesCompleted)
            {
                stagesCompleted = stageCompleted.StageIndex;
                SaveStageCompleted();
            }
        }
    }
}
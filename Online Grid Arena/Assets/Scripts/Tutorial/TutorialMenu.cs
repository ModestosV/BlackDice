using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class TutorialMenu: HideableUI, IEventSubscriber
{
    private const int MAX_STAGE_ALLOWED = 4;

    private string filepath = "";
    private int stagesCompleted = 0;

    public List<Button> StageButtons;

    private void Start()
    {
        filepath = Application.persistentDataPath + "/playerInfo.bin";
        Debug.Log(filepath);
        HandleEventBus();

        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.tag.Equals("StageButton"))
            {
                StageButtons.Add(button);
            }
        }

        VerifySaveFileExist();

        LoadTutorialMenu();
    }

    public void HandleEventBus()
    {
        EventBus.Reset();
        EventBus.Subscribe<StageCompletedEvent>(this);
    }

    public void LoadTutorialMenu()
    {
        stagesCompleted = ReadStageCompleted();

        for (int i = 0; i < stagesCompleted; i++)
        {
            StageButtons[i].interactable = true;
            StageButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        }

        if (stagesCompleted < MAX_STAGE_ALLOWED)
        {
            StageButtons[stagesCompleted].interactable = true;
            StageButtons[stagesCompleted].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
        }

        Show();
    }

    public void PlayTutorialStage(int stageIndex)
    {
        HandleEventBus();
        SceneManager.LoadScene(stageIndex);
    }

    public void ExitToMainMenu()
    {

        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null)
            Destroy(music);

        Hide();

        SceneManager.LoadScene(0);
    }

    private void VerifySaveFileExist()
    {
        if (!File.Exists(filepath))
        {
            TutorialSerializedObject tutorialSerializedObject = new TutorialSerializedObject() { HighestStageCompleted = stagesCompleted };
            BinarySerialization.WriteToBinaryFile<TutorialSerializedObject>(filepath, tutorialSerializedObject);
        }
    }

    private void SaveStageCompleted()
    {
        TutorialSerializedObject tutorialSerializedObject = new TutorialSerializedObject() { HighestStageCompleted = stagesCompleted };
        BinarySerialization.WriteToBinaryFile<TutorialSerializedObject>(filepath, tutorialSerializedObject);
    }

    private int ReadStageCompleted()
    {
        TutorialSerializedObject tutorialSerializedObject = BinarySerialization.ReadFromBinaryFile<TutorialSerializedObject>(filepath);
        return tutorialSerializedObject.HighestStageCompleted;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(StageCompletedEvent))
        {
            var stageCompleted = (StageCompletedEvent)@event;

            Debug.Log(stageCompleted.StageIndex);

            if (stageCompleted.StageIndex > stagesCompleted)
            {
                stagesCompleted = stageCompleted.StageIndex;
                SaveStageCompleted();
            }
        }
    }
}
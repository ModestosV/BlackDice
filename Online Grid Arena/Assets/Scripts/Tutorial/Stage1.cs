﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : HideableUI
{
    void Start()
    {
        Show();
    }

    public void CompleteStage1()
    {
        EventBus.Publish(new StageCompletedEvent(1));

        ExitToMainMenu();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
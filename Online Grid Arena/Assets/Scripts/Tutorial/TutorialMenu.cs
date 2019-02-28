using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialMenu: HideableUI
{

    public void LoadTutorialMenu()
    {
        Show();
    }

    public void PlayTutorialStage1()
    {
        SceneManager.LoadScene(2);
    }
}
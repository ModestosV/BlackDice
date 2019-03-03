using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : HideableUI
{
    void Start()
    {
        Show();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
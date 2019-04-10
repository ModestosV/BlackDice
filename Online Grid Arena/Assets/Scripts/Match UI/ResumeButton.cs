using UnityEngine;
using UnityEngine.UI;

public sealed class ResumeButton : MonoBehaviour
{
    private IMatchMenu matchMenu;
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
        matchMenu = FindObjectOfType<MatchMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        matchMenu = FindObjectOfType<MatchMenu>();
        button.onClick.AddListener(Resume);
    }

    private void Resume()
    {
        matchMenu.Toggle();
    }
}
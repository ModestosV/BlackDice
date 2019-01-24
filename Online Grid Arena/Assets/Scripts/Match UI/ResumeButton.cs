using UnityEngine;
using UnityEngine.UI;

public sealed class ResumeButton : MonoBehaviour
{
    public IMatchMenu MatchMenu { private get; set; }
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        MatchMenu = FindObjectOfType<MatchMenu>();
        button.onClick.AddListener(Resume);
    }

    private void Resume()
    {
        MatchMenu.Toggle();
    }
}
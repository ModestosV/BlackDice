using UnityEngine;
using UnityEngine.UI;

public sealed class ControlsButton : MonoBehaviour
{
    public IControlsMenu ControlsMenu { private get; set; }
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
        ControlsMenu = FindObjectOfType<ControlsMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        ControlsMenu = FindObjectOfType<ControlsMenu>();
        button.onClick.AddListener(ToggleControlsMenu);
    }

    private void ToggleControlsMenu()
    {
        ControlsMenu.Toggle();
    }
}
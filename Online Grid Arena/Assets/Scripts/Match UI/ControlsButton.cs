using UnityEngine;
using UnityEngine.UI;

public sealed class ControlsButton : MonoBehaviour
{
    private IControlsMenu controlsMenu;
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
        controlsMenu = FindObjectOfType<ControlsMenu>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        controlsMenu = FindObjectOfType<ControlsMenu>();
        button.onClick.AddListener(ToggleControlsMenu);
    }

    private void ToggleControlsMenu()
    {
        controlsMenu.Toggle();
    }
}
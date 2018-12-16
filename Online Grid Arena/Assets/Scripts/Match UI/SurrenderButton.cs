using UnityEngine;
using UnityEngine.UI;

public sealed class SurrenderButton : MonoBehaviour
{
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Surrender);
	}

    private void Surrender()
    {
        EventBus.Publish(new SurrenderEvent());
    }
}

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : BlackDiceMonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private AbilityTooltip tooltip;

    public string Description { private get; set; }
    public int Cooldown { private get; set; }
    public int Index { private get; set; }

    void Start()
    {
        Text buttonText = this.GetComponentInChildren<Text>();
        buttonText.transform.SetAsLastSibling();
        tooltip = FindObjectOfType<AbilityTooltip>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Publish(new AbilityClickEvent(Index));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Cooldown > 0)
        {
            tooltip.ShowTooltip(Description, Cooldown);
        }
        else
        {
            tooltip.ShowTooltip(Description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }
}

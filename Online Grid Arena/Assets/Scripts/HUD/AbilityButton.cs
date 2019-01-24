using UnityEngine.EventSystems;

public class AbilityButton : BlackDiceMonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    private AbilityTooltip tooltip;

    public string Description { get; set; }
    public int Cooldown { get; set; }
    public int Index { get; set; }

    void Start()
    {
        Text buttonText = this.GetComponentInChildren<Text>();
        buttonText.transform.SetAsLastSibling();
        tooltip = FindObjectOfType<AbilityTooltip>();
    }
<<<<<<< HEAD
=======

    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Publish(new AbilityClickEvent(Index));
    }
>>>>>>> #107 cleanup of endTurnButton and make clickable the abilities

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

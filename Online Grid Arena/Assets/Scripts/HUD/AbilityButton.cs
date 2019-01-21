using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GraphicRaycaster ray;
    private AbilityTooltip tooltip;

    public string Description { get; set; }
    public int Cooldown { get; set; }
    
	void Start ()
    {
        ray = this.GetComponent<GraphicRaycaster>();
        tooltip = FindObjectOfType<AbilityTooltip>();
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

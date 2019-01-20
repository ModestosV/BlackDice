using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GraphicRaycaster ray;
    public AbilityToolTip tooltip;
    
	void Start ()
    {
        ray = this.GetComponent<GraphicRaycaster>();
        tooltip = FindObjectOfType<AbilityToolTip>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }
}

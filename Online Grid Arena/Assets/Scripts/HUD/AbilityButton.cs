﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GraphicRaycaster ray { get; set; }
    private AbilityTooltip tooltip { get; set; }

    public string Description;
    public int Cooldown;
    
	void Start ()
    {
        ray = this.GetComponent<GraphicRaycaster>();
        tooltip = FindObjectOfType<AbilityTooltip>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Cooldown > 0)
        {
            tooltip.ShowToolTip(Description, Cooldown);
        }
        else
        {
            tooltip.ShowToolTip(Description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }
}

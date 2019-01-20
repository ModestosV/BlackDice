using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class EffectStack : HideableUI, IPointerEnterHandler, IPointerExitHandler
{
    public string Description;
    public int Stacks;
    public GraphicRaycaster ray;
    public AbilityToolTip tooltip;

    public Text stackIndicator;

    public void Start()
    {
        ray = this.GetComponent<GraphicRaycaster>();
        tooltip = FindObjectOfType<AbilityToolTip>();
        stackIndicator = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void Update()
    {
        if (Stacks > 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void UpdateStacks(int stacks)
    {
        Stacks = stacks;
        stackIndicator.text = Stacks.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowToolTip(Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }

}

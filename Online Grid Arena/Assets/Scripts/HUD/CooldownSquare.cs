using UnityEngine;
using UnityEngine.UI;
using System;

public class CooldownSquare : HideableUI
{
    [SerializeField]
    public Text SquareText { protected get; set; }

    void Start()
    {
        Hide();
        SquareText = GetComponentInChildren<Text>();
    }

    public void UpdateSquare(int cooldown, int cooldownRemaining)
    {
        if (cooldown != 0)
        {
            SquareText.text = cooldownRemaining.ToString();
        }
        Show(); 
    }
}
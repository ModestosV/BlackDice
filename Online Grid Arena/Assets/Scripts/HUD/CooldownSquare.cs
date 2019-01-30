using UnityEngine;
using UnityEngine.UI;
using System;

public class CooldownSquare : HideableUI
{
    [SerializeField]
    public String SquareText { protected get; set; }

    void Start()
    {
        Hide();
    }

    public void UpdateSquare(bool isOnCooldown, int cooldownRemaining, Text text)
    {
        if (SquareText == null)
        {
            SquareText = text.text;
        }

        if (isOnCooldown)
        {
            text.text = cooldownRemaining.ToString();
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 30;
            text.fontStyle = FontStyle.Bold;
            Show(); 
        }
        else
        {
            text.color = Color.black;
            text.alignment = TextAnchor.LowerLeft;
            text.text = SquareText;
            text.fontStyle = FontStyle.Normal;
            text.fontSize = 14;
            Hide();
        }
    }
}
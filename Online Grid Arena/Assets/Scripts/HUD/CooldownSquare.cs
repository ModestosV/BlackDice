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

    public void UpdateSquare(bool changeText, int cooldownRemaining, Text previousText)
    {
        if (SquareText == null)
        {
            SquareText = previousText.text;
        }

        if (changeText)
        {
            previousText.text = cooldownRemaining.ToString();
            previousText.color = Color.white;
            previousText.alignment = TextAnchor.MiddleCenter;
            previousText.fontSize = 30;
            previousText.fontStyle = FontStyle.Bold;
            Show(); 
        }
        else
        {
            previousText.color = Color.black;
            previousText.alignment = TextAnchor.LowerLeft;
            previousText.text = SquareText;
            previousText.fontStyle = FontStyle.Normal;
            previousText.fontSize = 14;
            Hide();
        }
    }
}
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
            Show(); 
        }
        else
        {
            previousText.color = Color.black;
            previousText.text = SquareText;
            Hide();
        }
    }
}
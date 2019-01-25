using UnityEngine;
using UnityEngine.UI;
using System;

public class CooldownSquare : HideableUI
{
    [SerializeField]
    public Color SquareColor {protected get; set;}

    void Start()
    {
        Hide();
        SquareColor = GetComponent<Image>().color;
    }

    public void UpdateSquare(int cooldown, int cooldownRemaining)
    {
        Color color = SquareColor;
        int alpha = 0;
        if (cooldown != 0)
        {
            alpha = (cooldownRemaining / cooldown);
        }
        color.a = Convert.ToByte(alpha);
        SquareColor = color;
        Show();
         
    }
}
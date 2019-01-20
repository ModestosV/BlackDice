using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EffectStack : HideableUI
{
    public string Description;
    public int Stacks;

    public Text stackIndicator;

    public void Start()
    {
        stackIndicator = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void Update()
    {
        if (Stacks > 0)
        {
            Show();
        }
    }

    public void UpdateStacks(int stacks)
    {
        Stacks = stacks;
        stackIndicator.text = Stacks.ToString();
    }

}

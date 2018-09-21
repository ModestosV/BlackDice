﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCharacterStats : MonoBehaviour
{

    [SerializeField] public GameObject charStats;
    private bool showPanel = false;
    Ray ray;
    RaycastHit hit;

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
            CheckToggle();

    }

    private void CheckToggle()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (showPanel)
            {
                showPanel = false;
                charStats.gameObject.SetActive(false);
            }
            else
            {
                showPanel = true;
                charStats.gameObject.SetActive(true);
            }
        }
    }
}

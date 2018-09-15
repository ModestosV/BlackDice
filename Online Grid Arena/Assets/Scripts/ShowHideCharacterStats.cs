using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCharacterStats : MonoBehaviour {

    [SerializeField] public GameObject charStats;
    private bool showPanel = false;

    public void Update()
    {
        // Toggles character stats panel on right click
        if (Input.GetMouseButtonDown(1))
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

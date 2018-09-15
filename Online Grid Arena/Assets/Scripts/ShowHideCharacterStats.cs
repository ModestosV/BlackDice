using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideCharacterStats : MonoBehaviour {

    [SerializeField] public GameObject charStats;
    private bool showPanel = false;

    Ray ray;
    RaycastHit hit;

    public void Update()
    {
        // Toggles character stats panel on right click
        if (Input.GetMouseButtonDown(1))
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
}

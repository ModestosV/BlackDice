using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public abstract class Panel : MonoBehaviour {

    public GameObject loadingCircle;
    public TextMeshProUGUI StatusText;

    public void ActivateLoadingCircle()
    {
        loadingCircle.SetActive(true);
    }

    public void DeactivateLoadingCircle()
    {
        loadingCircle.SetActive(false);
    }

    public void SetStatus(string statusText)
    {
        StatusText.text = statusText;
    }

    public void ClearStatus()
    {
        StatusText.text = "";
    }
}

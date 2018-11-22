using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public abstract class Panel : MonoBehaviour {

    public GameObject loadingCircle;

    public void ActivateLoadingCircle()
    {
        loadingCircle.SetActive(true);
    }

    public void DeactivateLoadingCircle()
    {
        loadingCircle.SetActive(false);
    }

    public abstract void SetStatus(string statusText);

    public abstract void ClearStatus();
}

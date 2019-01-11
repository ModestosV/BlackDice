using UnityEngine;
using TMPro;

public abstract class Panel : MonoBehaviour
{
    protected LoadingCircle loadingCircle;
    protected TextMeshProUGUI statusText;

    public void ActivateLoadingCircle()
    {
        loadingCircle.gameObject.SetActive(true);
    }

    public void DeactivateLoadingCircle()
    {
        loadingCircle.gameObject.SetActive(false);
    }

    public void SetStatus(string statusText)
    {
        this.statusText.text = statusText;
    }

    public void ClearStatus()
    {
        statusText.text = "";
    }
}

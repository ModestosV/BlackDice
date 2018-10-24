using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI webText;

	public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

    public void FetchWebData()
    {
        StartCoroutine(GetTextFromWeb());
    }

    IEnumerator GetTextFromWeb()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://www.randomtext.me/api/"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                webText.text = www.downloadHandler.text;

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}

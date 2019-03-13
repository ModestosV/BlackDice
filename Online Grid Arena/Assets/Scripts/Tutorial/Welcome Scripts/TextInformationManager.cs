using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInformationManager : MonoBehaviour
{
    public TextInformation textInformation;

    public Text infoBlob;

    private List<GameObject> tiles;
    private List<string> infoSegment;
    private int nextInfo;

    void Start()
    {
        tiles = new List<GameObject>();
        infoSegment = new List<string>();
        nextInfo = -1;
        StartInfoSegment(textInformation);
    }

    public void StartInfoSegment(TextInformation textInfo)
    {
        Debug.Log("Information accessed");

        infoSegment.Clear();

        foreach (string info in textInfo.infoBlobs)
        {
            infoSegment.Add(info);
        }

        DisplayNextInfoBlob();
    }

    public void DisplayNextInfoBlob()
    {
        Debug.Log("Display next info");

        if (nextInfo == infoSegment.Count - 1)
        {
            nextInfo = 0;
        }
        else
        {
            nextInfo++;
        }

        if (tiles.Count > 0)
        {
            GameObject tile = tiles[nextInfo];
        }

        string info = infoSegment[nextInfo];
        infoBlob.text = info;
        Debug.Log("Next info displayed");
    }

    public void DisplayPrevInfoBlob()
    {
        Debug.Log("Display previous info");
        if (nextInfo == 0)
        {
            nextInfo = infoSegment.Count - 1;
        }
        else
        {
            nextInfo--;
        }

        if (tiles.Count > 0)
        {
            GameObject tile = tiles[nextInfo];
        }

        string info = infoSegment[nextInfo];
        infoBlob.text = info;
        Debug.Log("Previous info displayed");
    }

}

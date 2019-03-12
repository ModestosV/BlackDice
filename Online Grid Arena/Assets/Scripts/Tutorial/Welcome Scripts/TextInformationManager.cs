using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInformationManager : MonoBehaviour
{
    private List<string> infoSegment;
    private int nextInfo;

    void Start()
    {
        infoSegment = new List<string>();
        nextInfo = 0;
        //StartInfoSegment();
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
        if (nextInfo == infoSegment.Count - 1)
        {
            nextInfo = 0;
        }

        string info = infoSegment[nextInfo];
        nextInfo++;
    }

    public void DisplayPrevInfoBlob()
    {
        if (nextInfo == 0)
        {
            nextInfo = infoSegment.Count - 1;
        }

        string info = infoSegment[nextInfo];
        nextInfo--;
    }

}

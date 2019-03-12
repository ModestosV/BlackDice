using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextInformation
{
    public string section;

    [TextArea(1, 15)]
    public string[] infoBlobs;
}

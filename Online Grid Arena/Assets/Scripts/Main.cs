﻿using UnityEngine;

public class Main : MonoBehaviour
{
	void Start ()
    {
        ILogger devLogger = new DevLogger();
        devLogger.Log(LogLevel.INFO, "This is how you use the dev logger");
    }
}

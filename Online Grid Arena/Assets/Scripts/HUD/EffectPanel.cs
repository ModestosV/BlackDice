using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPanel : MonoBehaviour, IEffectPanel
{
    public IEffectPanelController Controller { get; set; }

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    void Awake()
    {
        Controller = new EffectPanelController(); //add more to this

    }

    void Start()
    {

    }
}

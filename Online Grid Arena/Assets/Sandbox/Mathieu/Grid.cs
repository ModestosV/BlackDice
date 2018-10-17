using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GridController Controller;

    private void OnValidate()
    {
        Controller.Init();
        Controller.SetHexTiles(GetComponentsInChildren<HexTile2>());
    }
    
    void Start () {
		
	}
	
	void Update () {
		
	}
}

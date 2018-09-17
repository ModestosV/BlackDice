using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    Material currentMat;
    public Material[] materials; //for now 0 is def and 1 is light blue
    MeshRenderer rend;
    Vector3 size;
    bool isClicked;
	// Use this for initialization
	void Start ()
    {
        isClicked = false;
        size = GetComponent<Collider>().bounds.size;
        rend = this.GetComponentInChildren<MeshRenderer>();
        currentMat = materials[0];
        rend.material = currentMat;
        Debug.Log(size.x+"x");
        Debug.Log(size.y+"y");
        Debug.Log(size.z+"z");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnMouseEnter()
    {
        if (!isClicked)
        { 
            currentMat = materials[1];
            rend.material = currentMat;
        }
    }

    private void OnMouseExit()
    {
        if (!isClicked)
        {
            currentMat = materials[0];
            rend.material = currentMat;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(transform.position.y);
        if (isClicked)
        {
            currentMat = materials[1];
            rend.material = currentMat;
            isClicked = !isClicked;
        }
        else
        {
            currentMat = materials[2];
            rend.material = currentMat;
            isClicked = !isClicked;
        }
    }

}

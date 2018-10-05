using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private GridMap gridReference;
    public int X
    {
        get;
        set;
    }
    public int Y
    {
        get;
        set;
    }
    public int Z
    {
        get;
        set;
    }

    private GameObject occupant;
    public Material currentMat;
    public Material[] materials; //for now 0 is def and 1 is light blue
    public MeshRenderer rend;
    Vector3 size;
    bool isClicked;

	// Use this for initialization
	void Start ()
    {
        occupant = null;
        isClicked = false;
        size = GetComponent<Collider>().bounds.size;
        rend = this.GetComponentInChildren<MeshRenderer>();

        if(materials == null)
        {
            Debug.LogWarning("Materials not specified!");
        }
        currentMat = materials[0];

        if(rend == null)
        {
            Debug.LogWarning("No mesh renderer!");

        }
        rend.material = currentMat;
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void setCoords(int newX, int newY, int newZ, int newW)
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
        GameObject[] myarr = gridReference.getRowFull(this.gameObject);
        
        for (int i = 0; i < myarr.Length; i++)
        {
            if (myarr[i] != null)
            {
                Debug.Log("Destroying object" + i);
                Destroy(myarr[i]);
            }
        }
        Debug.Log(isClicked);
        if (isClicked)
        {
            //currentMat = materials[1];
            //rend.material = currentMat;
            isClicked = !isClicked;
            gridReference.setClicked(null);
        }
        else
        {
            //currentMat = materials[2];
            //rend.material = currentMat;
            isClicked = !isClicked;
            gridReference.setClicked(this);
        }
        //Debug.Log("TILE INFO: (column number)x = "+X+" /y = "+Y+" /z = "+Z);
        Debug.Log(isClicked);
    }

    public void setOccupant(GameObject occ)
    {
        occupant = occ;
    }

    public GameObject getOccupant()
    {
        return occupant;
    }

    public void setGrid(GridMap refGrid)
    {
        gridReference = refGrid;
    }

    public bool getIsClicked()
    {
        return isClicked;
    }

    public void setIsClicked(bool newState)
    {
        isClicked = newState;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private GridMap gridReference;
    private int x, y, z; //x is column number, y is left diagonal, z is right diagonal all of them -3 to 3
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
        currentMat = materials[0];
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
        //testing code for knowing if methods work properly
        /**GameObject[] myarr = gridReference.getLeftDiagonal(this.gameObject);
        for (int i = 0; i < 7; i++)
        {
            if (myarr[i] != null)
            {
                Destroy(myarr[i]);
            }
        }*/
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
        Debug.Log("TILE INFO: (column number)x = "+x+" /y = "+y+" /z = "+z);
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

    public int getX()
    {
        return x;
    }

    public void setX(int newX)
    {
        x = newX;
    }

    public int getY()
    {
        return y;
    }

    public void setY(int newY)
    {
        y = newY;
    }

    public int getZ()
    {
        return z;
    }

    public void setZ(int newZ)
    {
        z = newZ;
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

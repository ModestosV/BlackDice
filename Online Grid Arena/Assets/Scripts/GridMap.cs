﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using System;

public class GridMap : MonoBehaviour {

    private int height = 7;
    private int width = 7;

    //triple coordinate system
    private double x; // x is column number coordinate 
    private int y, z, w; // for setting tile info
    private int colNum; // number of columns in total
    public GameObject tilePrefab;
    Vector3 tileSize;
    GameObject[][] grid;
    private Dictionary<Tuple<int,int,int>, GameObject> myGrid;

	void Start () {
        myGrid = new Dictionary<Tuple<int, int, int>, GameObject>();
        colNum = 7;
        x = -3;
        y = 0;
        grid = new GameObject[colNum][];//num of cols
        for (int i = 0; i < colNum; i++) // 0 to 6
        {
            grid[i] = new GameObject[7-Mathf.Abs((int)x)]; //column sizes: 4,5,6,7,6,5,4
            x++;
        }

        tileSize = tilePrefab.GetComponent<Collider>().bounds.size;

        x = -3; //reset x so that we may reinstantiate at the right place
        for (int index = 0; index < colNum; index++) //for each array of varying size of tiles
        {
            w = Mathf.Abs((int)x);
            if (x <= 0)
            {
                y = 3;
                z = Mathf.Abs((int)x) - 3;
            }
            else
            {
                y = 3-(int)x;
                z = -3;
            }
            for (int j = 0; j < grid[index].Length; j++) //for each tile in that column
            {
                GameObject current = Instantiate(tilePrefab, new Vector3(index-(float)x/4, j+(Mathf.Abs((float)x)/2)), Quaternion.Euler(90, 0, 0));
                HexTile tile = current.GetComponent<HexTile>();
                tile.setX((int)x);
                tile.setY((int)y);
                tile.setZ(z);
                myGrid.Add(new Tuple<int,int,int>(tile.getX(),tile.getY(),tile.getZ()),current);
                y--;
                z++;
            }
            x++;
        }
        grid = null;
	}
	
	void Update ()
    {
		
	}

    public GameObject[] getNeighbors(GameObject tileO) //returns all neighbors starting from top,going clockwise
    {
        HexTile tile = tileO.GetComponent<HexTile>();
        GameObject[] res = new GameObject[6];
        myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX(), tile.getY() - 1, tile.getZ() + 1), out res[0]);
        myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX() + 1, tile.getY() - 1, tile.getZ()), out res[1]);
        myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX() + 1, tile.getY(), tile.getZ() - 1), out res[2]);
        myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX(), tile.getY() + 1, tile.getZ() - 1), out res[3]);
        myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX() - 1, tile.getY() + 1, tile.getZ()), out res[4]);
        myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX() - 1, tile.getY(), tile.getZ() + 1), out res[5]);
        return res;
    }

    public GameObject[] getColumn() //returns entire column, write method twice so it could take object or int as param. same thing for all the others
    {
        return null;
    }

    public GameObject[] getRightDiagonal() //returns entire diagonal that goes from bottom left to top right
    {
        return null;
    }

    public GameObject[] getLeftDiagonal() //returns entire diagonal that goes from top left to bottom right
    {
        return null;
    }

    public GameObject[] getRowSkip() // returns entire row of tiles that are exactly at the same height (i.e. array will not hold tiles from every row)
    {
        return null;
    }

    public GameObject[] getRowFull() //return full row of tiles that are within +/= 0.5 of eachother (i.e. will have 1 tile from 1 row, 2 from the next, etc.etc.)
    {
        return null;
    }

    public GameObject getTile(int x, int y, int z)
    {
        GameObject res = null;
        myGrid.TryGetValue(new Tuple<int,int,int>(x,y,z), out res);
        return res;
    }

}

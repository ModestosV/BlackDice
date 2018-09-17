using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour {

    private int height = 7;
    private int width = 7;

    //triple coordinate system
    private double x; // x is column number coordinate 
    private int colNum; // number of columns in total
    public GameObject tilePrefab;
    Vector3 tileSize;
    //GameObject[,] grid;
    GameObject[][] grid;

	void Start () {
        colNum = 7;
        x = -3;
        grid = new GameObject[colNum][];//num of cols
        for (int i = 0; i < colNum; i++) // 0 to 6
        {
            grid[i] = new GameObject[7-Mathf.Abs((int)x)]; //column sizes: 4,5,6,7,6,5,4
            x++;
        }

        //grid = new GameObject[width, height];
        tileSize = tilePrefab.GetComponent<Collider>().bounds.size;

        // Debug.Log("tile on column "+x+" and row "+y+" has been instantiated at location (x,y)=("+x+","+y+")");


        //here try to make a grid in hex shape
        x = -3; //reset x so that we may reinstantiate at the right place? 
        for (int index = 0; index < colNum; index++) //for each array of varying size of tiles
        {
            for (int j = 0; j < grid[index].Length; j++) //for each tile in that column
            {
                Instantiate(tilePrefab, new Vector3(index-(float)x/4, j+(Mathf.Abs((float)x)/2)), Quaternion.Euler(90, 0, 0));
            }
            x++;
        }

	}
	
	void Update ()
    {
		
	}
}

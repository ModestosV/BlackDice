using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using System;

public class GridMap : MonoBehaviour {

    private int height = 7;
    private int width = 7;

    private static HexTile clickedCell; 

    //triple coordinate system
    private double x; // x is column number coordinate 
    private int y, z, w; // for setting tile info
    private int colNum; // number of columns in total
    public GameObject tilePrefab;
    Vector3 tileSize;
    GameObject[][] grid;
    private Dictionary<Tuple<int,int,int>, GameObject> myGrid;

	void Start () {
        clickedCell = null;
        myGrid = new Dictionary<Tuple<int, int, int>, GameObject>();
        colNum = 7;
        x = -3;
        y = 0;
        grid = new GameObject[colNum][];//num of cols
        for (int i = 0; i < colNum; i++) // 0 to 6
        {
            grid[i] = new GameObject[height-Mathf.Abs((int)x)]; //column sizes: 4,5,6,7,6,5,4
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
                tile.setGrid(this);
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

    public GameObject[] getColumn(GameObject tile0) //returns entire column, write method twice so it could take object or int as param. same thing for all the others
    {
        HexTile tile = tile0.GetComponent<HexTile>();
        GameObject[] column = new GameObject[height];
        double lengthColumn = height - Math.Abs(tile.getX());
        int MaxV = (int)Math.Floor((double)height / 2);
        for (int i = 0; i < height; i++)
        {
            if (tile.getX() == 0)
            {
                myGrid.TryGetValue(new Tuple<int, int, int>(0, MaxV - i, -MaxV + i), out column[i]);
            }
            else if (tile.getX() < 0)
            {
                if (i < lengthColumn)
                {
                    myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX(), MaxV - i, -MaxV - tile.getX() + i), out column[i]);
                }
                else
                {
                    column[i] = null;
                }
            }
            else if (tile.getX() > 0)
            {
                if (i < lengthColumn)
                {
                    myGrid.TryGetValue(new Tuple<int, int, int>(tile.getX(), MaxV - tile.getX() - i, -MaxV + i), out column[i]);
                }
                else
                {
                    column[i] = null;
                }
            }
        }
        return column;
    }

    public GameObject[] getColumn(int columnNum) //returns entire column, write method twice so it could take object or int as param. same thing for all the others
    {
        GameObject[] column = new GameObject[height];
        double lengthColumn = height - Math.Abs(columnNum);
        int MaxV = (int)Math.Floor((double)height / 2);
        for (int i = 0; i < height; i++)
        {
            if (columnNum == 0)
            {
                myGrid.TryGetValue(new Tuple<int, int, int>(0, MaxV - i, -MaxV + i), out column[i]);
            }
            else if (columnNum < 0)
            {
                if (i < lengthColumn)
                {
                    myGrid.TryGetValue(new Tuple<int, int, int>(columnNum, MaxV - i, -MaxV - columnNum + i), out column[i]);
                }
                else
                {
                    column[i] = null;
                }
            }
            else if (columnNum > 0)
            {
                if (i < lengthColumn)
                {
                    myGrid.TryGetValue(new Tuple<int, int, int>(columnNum, MaxV - columnNum - i, -MaxV + i), out column[i]);
                }
                else
                {
                    column[i] = null;
                }
            }
        }
        return column;
    }

    public GameObject[] getRightDiagonal(GameObject tile) //returns entire diagonal that goes from bottom left to top right
    {
        HexTile tile0 = tile.GetComponent<HexTile>();
        GameObject[] res = new GameObject[height];
        int total = tile0.getZ()*-1;
        for (int i = 0; i < height; i++)//0 to 6
        {
            int currentX = i - 3;
            //now we know x+y = total, so
            int currentY = total - currentX;
            myGrid.TryGetValue(new Tuple<int, int, int>(currentX, currentY, total*-1), out res[i]);
        }
        return res;
    }

    public GameObject[] getLeftDiagonal(GameObject tile) //returns entire diagonal that goes from top left to bottom right
    {
        HexTile tile0 = tile.GetComponent<HexTile>();
        GameObject[] res = new GameObject[height];
        int total = tile0.getY() * -1;
        for (int i = 0; i < height; i++)//0 to 6
        {
            int currentX = i - 3;
            //now we know x+z = total, so
            int currentZ = total - currentX;
            myGrid.TryGetValue(new Tuple<int, int, int>(currentX, total*-1, currentZ), out res[i]);
        }
        return res;
    }

    public GameObject[] getRowSkip(GameObject tile0) // returns entire row of tiles that are exactly at the same height (i.e. array will not hold tiles from every row)
    {
        HexTile tile = tile0.GetComponent<HexTile>();
        GameObject[] row = new GameObject[height];
        int maxV = (int)Math.Floor((double)height / 2);
        int yzInc = 0;
        int xvalue = tile.getX();
        int yvalue = tile.getY();

        while (xvalue != -maxV && xvalue != -maxV + 1)
        {
            xvalue -= 2;
            yvalue++;
        }

        for (int i = (Math.Abs(tile.getX()%2) == maxV%2)? 0:1; i<height; i+=2)
        {
            myGrid.TryGetValue(new Tuple<int, int, int>(i - maxV, yvalue - yzInc, ((-i + maxV) - (yvalue - yzInc))), out row[i]);
            yzInc++;
        }

        return row;
    }

    //tile being called is always assumed to be in the middle of the full row. 
    public GameObject[] getRowFull(GameObject tile0) //return full row of tiles that are within +/= 0.5 of eachother (i.e. will have 1 tile from 1 row, 2 from the next, etc.etc.)
    {
        HexTile tile = tile0.GetComponent<HexTile>();
        int maxRowSize = (int)Math.Ceiling((double)height/2);
        List<GameObject> res = new List<GameObject>();
        GameObject[] neighbors = this.getNeighbors(tile0);
        GameObject[] middleRow = this.getRowSkip(tile0);

        for (int i = 0; i < middleRow.Length; i++)
        {
            res.Add(middleRow[i]);
        }
        if (tile.getX() == 3 || tile.getY() == -3 || tile.getZ() == -3) //want top left and bottom left 4 and 5
        {
            if (neighbors[5] != null)
            {
                GameObject[] topRow = this.getRowSkip(neighbors[5].gameObject);
                foreach (GameObject g in topRow)
                {
                    res.Add(g);
                }
            }
            if (neighbors[4] != null)
            {
                GameObject[] bottomRow = this.getRowSkip(neighbors[4].gameObject);
                foreach (GameObject g in bottomRow)
                {
                    res.Add(g);
                }
            }
        }
        else //top right and bottom right 1 and 2
        {
            if (neighbors[1] != null)
            {
                GameObject[] topRow = this.getRowSkip(neighbors[1].gameObject);
                foreach (GameObject g in topRow)
                {
                    res.Add(g);
                }
            }
            if (neighbors[2] != null)
            {
                GameObject[] bottomRow = this.getRowSkip(neighbors[2].gameObject);
                foreach (GameObject g in bottomRow)
                {
                    res.Add(g);
                }
            }
        }
        return res.ToArray();
    }

    public GameObject getTile(int x, int y, int z)
    {
        GameObject res = null;
        myGrid.TryGetValue(new Tuple<int,int,int>(x,y,z), out res);
        return res;
    }

    public void setClicked(HexTile tile)
    {
        if (tile == null)
        {
            clickedCell.setIsClicked(!clickedCell.getIsClicked());
        }

         if (clickedCell != null)
        {
            //make it go back to normal color
            clickedCell.setIsClicked(!clickedCell.getIsClicked());
            clickedCell.currentMat = clickedCell.materials[0];
            clickedCell.rend.material = clickedCell.currentMat;
        }
        clickedCell = tile; //set the new one and change its color to clicked mode
        if (clickedCell != null)
        {
            clickedCell.currentMat = clickedCell.materials[2];
            clickedCell.rend.material = clickedCell.currentMat;
        }

    }

}

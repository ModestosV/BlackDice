using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName= "Hex Grid/Grid Map")]
public class GridMapController : ScriptableObject,IGrid
{
    private int x;
    private int y, z;
    private int columnNumber;
    private GameObject[][] grid;
    private Dictionary<Tuple<int, int, int>, GameObject> gridDictionary;

    public int height = 7;
    public GameObject tilePrefab;

	public void InitializeMap()
    {
        gridDictionary = new Dictionary<Tuple<int, int, int>, GameObject>();
        columnNumber = height;
        int indexingValue = (int)Math.Floor((double)height/2);
        x = -indexingValue;
        y = 0;
        grid = new GameObject[columnNumber][];
        for (int i = 0; i < columnNumber; i++) 
        {
            grid[i] = new GameObject[height-Mathf.Abs(x)]; 
            x++;
        }

        x = -indexingValue; 
        for (int index = 0; index < columnNumber; index++) 
        {
            if (x <= 0)
            {
                y = indexingValue;
                z = Mathf.Abs(x) - indexingValue;
            }
            else
            {
                y = indexingValue - x;
                z = -indexingValue;
            }
            for (int j = 0; j < grid[index].Length; j++) 
            {
                GameObject current = Instantiate(tilePrefab, new Vector3(j+(Mathf.Abs((float)x)/2), 0, index - (float)x / 4), Quaternion.Euler(0, 90, 0));
                IHexTile tile = current.GetComponent<HexTile>().Tile;
                tile.SetGrid(this);
                tile.X= x;
                tile.Y= y;
                tile.Z= z;
                gridDictionary.Add(new Tuple<int, int, int>(tile.X,tile.Y,tile.Z),current);
                y--;
                z++;
            }
            x++;
        }
        grid = null;
	}
	
	public void Update ()
    {
		
	}

    public GameObject[] getNeighbors(GameObject tileObject) 
    {
        HexTileController tile = tileObject.GetComponent<HexTileController>();
        GameObject[] res = new GameObject[6];
        gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X, tile.Y - 1, tile.Z + 1), out res[0]);
        gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X + 1, tile.Y - 1, tile.Z), out res[1]);
        gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X + 1, tile.Y, tile.Z - 1), out res[2]);
        gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X, tile.Y + 1, tile.Z - 1), out res[3]);
        gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X - 1, tile.Y + 1, tile.Z), out res[4]);
        gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X - 1, tile.Y, tile.Z + 1), out res[5]);
        return res;
    }

    public GameObject[] getColumn(GameObject tileObject) 
    {
        HexTileController tile = tileObject.GetComponent<HexTileController>();
        GameObject[] column = new GameObject[height];
        for (int i = 0; i < height; i++)
        {
            gridDictionary.TryGetValue(new Tuple<int, int, int>(tile.X, i - 3, -i + 3 - tile.X), out column[i]);
        }
        return column;
    }

    public GameObject[] getColumn(int columnNum) 
    {
        GameObject[] column = new GameObject[height];
        for (int i = 0; i < height; i++)
        {
            gridDictionary.TryGetValue(new Tuple<int, int, int>(columnNum, i - 3, -i + 3 - columnNum), out column[i]);
        }
        return column;
    }

    public GameObject[] getRightDiagonal(GameObject tileObject) 
    {
        HexTileController tile = tileObject.GetComponent<HexTileController>();
        GameObject[] res = new GameObject[height];
        for (int i = 0; i < height; i++)
        {
            gridDictionary.TryGetValue(new Tuple<int, int, int>(i - 3, -i + 3 - tile.Z, tile.Z), out res[i]);
        }
        return res;
    }

    public GameObject[] getLeftDiagonal(GameObject tileObject) 
    {
        HexTileController tile = tileObject.GetComponent<HexTileController>();
        GameObject[] res = new GameObject[height];
        for (int i = 0; i < height; i++)
        {
            gridDictionary.TryGetValue(new Tuple<int, int, int>(i - 3, tile.Y, - i + 3 - tile.Y), out res[i]);
        }
        return res;
    }

    public GameObject[] getRowSkip(GameObject tileObject) 
    {
        HexTileController tile = tileObject.GetComponent<HexTileController>();
        GameObject[] row = new GameObject[height];
        int maxValue = (int)Math.Floor((double)height / 2);
        int yzInc = 0;
        int xValue = tile.X;
        int yValue = tile.Y;

        while (xValue != -maxValue && xValue != -maxValue + 1)
        {
            xValue -= 2;
            yValue++;
        }

        for (int i = (Math.Abs(tile.X%2) == maxValue%2)? 0:1; i<height; i+=2)
        {
            gridDictionary.TryGetValue(new Tuple<int, int, int>(i - maxValue, yValue - yzInc, ((-i + maxValue) - (yValue - yzInc))), out row[i]);
            yzInc++;
        }

        return row;
    }

    
    public GameObject[] getRowFull(GameObject tileObject) 
    {
        HexTileController tile = tileObject.GetComponent<HexTileController>();
        List<GameObject> res = new List<GameObject>();
        GameObject[] neighbors = this.getNeighbors(tileObject);
        GameObject[] middleRow = this.getRowSkip(tileObject);

        for (int i = 0; i < middleRow.Length; i++)
        {
            res.Add(middleRow[i]); 
        }
        if (tile.X == 3 || tile.Y == -3 || tile.Z == -3) 
        {
             res.AddRange(this.getIfNotNull(neighbors, 5, 4));
        }
        else 
        {
           res.AddRange(this.getIfNotNull(neighbors, 1, 2));
        }
        return res.ToArray();
    }

    public GameObject[] getIfNotNull(GameObject[] neighbors, int firstCheck, int secondCheck)
    {
        List<GameObject> res = new List<GameObject>();
        if (neighbors[firstCheck] != null)
        {
            GameObject[] topRow = this.getRowSkip(neighbors[firstCheck].gameObject);
            res.AddRange(topRow);
        }
        if (neighbors[secondCheck]!= null)
        {
            GameObject[] bottomRow = this.getRowSkip(neighbors[secondCheck].gameObject);
            res.AddRange(bottomRow);
        }
        return res.ToArray();
    }

    public GameObject getTile(int x, int y, int z)
    {
        GameObject res = null;
        gridDictionary.TryGetValue(new Tuple<int,int,int>(x,y,z), out res);
        return res;
    }   
    
    public void ClearSelection()
    {
        foreach(var tile in gridDictionary)
        {
            var tileComponent = tile.Value.GetComponent<HexTile>().Tile;
            tileComponent.SetIsClicked(false);
        }
    }
}

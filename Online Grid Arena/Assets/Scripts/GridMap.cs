using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour {

    private int height = 6;
    private int width = 6;

    public GameObject tilePrefab_even;
    public GameObject tilePrefab_odd;

	void Start () {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((x % 2) == 1) //if col is odd
                {
                    if ((y % 2) == 1) //if row is odd
                    {
                        Instantiate(tilePrefab_odd, new Vector3(x * 0.5f+0.26f, y*0.4f, 0), Quaternion.identity);
                    }
                    else //row even
                    {
                        Instantiate(tilePrefab_even, new Vector3(x * 0.5f, y * 0.4f, 0), Quaternion.identity);
                    }
                    
                }
                else //col even
                {
                    if ((y % 2) == 1) //if row is odd
                    {
                        Instantiate(tilePrefab_even, new Vector3(x * 0.5f+0.26f, y * 0.4f, 0), Quaternion.identity);
                    }
                    else //row even
                    {
                        Instantiate(tilePrefab_odd, new Vector3(x * 0.5f, y * 0.4f, 0), Quaternion.identity);
                    }
                }
            }
        }
	}
	
	void Update () {
		
	}
}

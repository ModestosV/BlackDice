using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour, IGrid
{
    public int gridWidth;
    private GridController controller;

    private void Awake()
    {
        controller = new GridController()
        {
            GridWidth = gridWidth
        };
        HexTile[] hexTiles = GetComponentsInChildren<HexTile>();
        ArrangeHexTilesInGridFormation(hexTiles);
        controller.GenerateGridMap(hexTiles.Select(x => x.Controller).ToList());
    }

    private void ArrangeHexTilesInGridFormation(HexTile[] hexTiles)
    {
        for (int i = 0; i < hexTiles.Length; i++)
        {
            IHexTile hexTile = hexTiles[i];
            int col = i % gridWidth;
            int row = i / gridWidth;
            
            Vector3 meshSize = hexTile.GameObject.GetComponent<Renderer>().bounds.size;
            float rowOffset = row % 2 == 0 ? 0.0f : meshSize.x / 2.0f;
            hexTile.GameObject.transform.position = new Vector3(col * meshSize.x + rowOffset, 0, -(row * 0.75f * meshSize.z));
        }
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}

using UnityEngine;

public class HexTileController : IHexTile
{

    private GridMapController gridReference;
    private GameObject occupant;
    private MeshRenderer rend;
    private bool isClicked;
    private HexTileDefinition tileDefinition;

    public int X {get;set;}
    public int Y {get;set;}
    public int Z {get;set;}

    private void SetCurrentMaterial(Material mat)
    {
        rend.material = mat;
    }

    public void InitializeTile(HexTileDefinition tileDefinition, MeshRenderer renderer)
    {
        occupant = null;
        isClicked = false;
        this.tileDefinition = tileDefinition;
        rend = renderer;
        rend.material = tileDefinition.DefaultMaterial;
    }

    public void OnMouseEnter()
    {
        if (!isClicked)
        {
            SetCurrentMaterial(tileDefinition.HoveredMaterial);
        }
    }

    public void OnMouseExit()
    {
        if (!isClicked)
        {
            SetCurrentMaterial(tileDefinition.DefaultMaterial);
        }
    }

    public void OnMouseDown()
    {
        if (!isClicked)
        {
            gridReference.ClearSelection();
        }

        toggleClickedState();
    }

    public void setOccupant(GameObject occupant)
    {
        this.occupant = occupant;
    }

    public GameObject getOccupant()
    {
        return occupant;
    }

    public void setGrid(GridMapController refGrid)
    {
        gridReference = refGrid;
    }

    public bool getIsClicked()
    {
        return isClicked;
    }

    public void toggleClickedState()
    {
        setIsClicked(!isClicked);
    }

    public void setIsClicked(bool newState)
    {
        isClicked = newState;
        SetCurrentMaterial(isClicked ? tileDefinition.ClickedMaterial : tileDefinition.DefaultMaterial);
    }
}

using UnityEngine;

public class HexTile : IHexTile
{

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

    public MeshRenderer rend;
    bool isClicked;
    private HexTileDefinition tileDefinition;

    public void InitializeTile(HexTileDefinition tileDefinition, MeshRenderer renderer)
    {
        occupant = null;
        isClicked = false;
        this.tileDefinition = tileDefinition;
        rend = renderer;

        currentMat = tileDefinition.DefaultMaterial;
        rend.material = currentMat;
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
        Debug.Log(isClicked);

        if (!isClicked)
        {
            gridReference.ClearSelection();
        }

        toggleClickedState();
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

    public void toggleClickedState()
    {
        setIsClicked(!isClicked);
    }

    public void setIsClicked(bool newState)
    {
        isClicked = newState;
        SetCurrentMaterial(isClicked ? tileDefinition.ClickedMaterial : tileDefinition.DefaultMaterial);
    }

    private void SetCurrentMaterial(Material mat)
    {
        currentMat = mat;
        rend.material = currentMat;
    }
}

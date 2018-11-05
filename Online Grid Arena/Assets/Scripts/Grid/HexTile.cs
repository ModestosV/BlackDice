using UnityEngine;

public class HexTile : MonoBehaviour, IHexTile
{
    private HexTileController controller;
    public HexTileMaterialSet materials;

    private void Awake()
    {
        controller = new HexTileController
        {
            OccupantCharacter = GetComponentInChildren<Character>().Controller,
            HexTile = this,
            IsEnabled = GetComponent<Renderer>().enabled
        };
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    #region IHexTile implementation

    public void SetHoverMaterial()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
    }

    public void SetErrorMaterial()
    {
        GetComponent<Renderer>().material = materials.HoveredErrorMaterial;
    }

    public void SetDefaultMaterial()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    public void SetClickedMaterial()
    {
        GetComponent<Renderer>().material = materials.ClickedMaterial;
    }

    public void SetHighlightMaterial()
    {
        GetComponent<Renderer>().material = materials.PathMaterial;
    }

    public bool IsMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<HexTile>() == this;
    }

    public IHexTileController Controller
    {
        get { return controller; }
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion

}



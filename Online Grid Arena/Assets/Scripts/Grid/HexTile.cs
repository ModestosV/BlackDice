using UnityEngine;

public class HexTile : MonoBehaviour, IHexTile
{
    public HexTileMaterialSet materials;

    private HexTileController hexTileController;

    private void Awake()
    {
        hexTileController = new HexTileController
        {
            HexTile = this,
            IsEnabled = GetComponent<Renderer>().enabled
        };
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    private void Start()
    {
        LinkOccupiedCharacter();
    }

    private void LinkOccupiedCharacter()
    {
        ICharacter occupantCharacter = GetComponentInChildren<Character>();

        if (occupantCharacter == null) return;

        occupantCharacter.Controller.OccupiedTile = hexTileController;
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
        get { return hexTileController; }
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion

}



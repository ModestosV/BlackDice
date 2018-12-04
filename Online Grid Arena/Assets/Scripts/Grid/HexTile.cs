using UnityEngine;

public sealed class HexTile : MonoBehaviour, IHexTile
{
<<<<<<< HEAD
    [SerializeField] private HexTileMaterialSet materials;
=======
    public HexTileMaterialSet materials;
    private Material Obstruction;
>>>>>>> #15 Add in Hex Tile Material Set a Obstruction Material

    private HexTileController hexTileController;

    private void Awake()
    {
        hexTileController = new HexTileController
        {
            HexTile = this,
            IsEnabled = GetComponent<Renderer>().enabled
        };
        Obstruction = null;
        GetComponent<Renderer>().material = materials.DefaultMaterial;


    }

    private void Start()
    {
        LinkOccupiedCharacter();
    }

    private void LinkOccupiedCharacter()
    {
        ICharacter occupantCharacter = GetComponentInChildren<AbstractCharacter>();

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

    public void SetObstruction()
    {
        GetComponent<Renderer>().material = materials.Obstruction;
    }

    public Material GetObstruction()
    {
        return Obstruction;
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
    
    public void PlayAbilityAnimation(GameObject abilityAnimationPrefab)
    {
        Instantiate(abilityAnimationPrefab, gameObject.transform);
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion

}



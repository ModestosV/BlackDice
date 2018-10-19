using UnityEngine;

public class SelectionController : MonoBehaviour, ICharacterSelectionController, ICharacterMovementController
{
    [SerializeField] public StatPanel statPanel;
    public ICharacter SelectedCharacter { get; set; }
    public IGridSelectionController GridSelectionController;

    private void Start()
    {
        GridSelectionController = FindObjectOfType<Grid>().Controller;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag != "Character" && hitObject.tag != "Tile") return;

            IHexTile hitTile = null;
            if (hitObject.tag == "Tile")
            {
                hitTile = hitObject.GetComponent<HexTile>();
                if (!hitTile.Controller().IsEnabled)
                {
                    GridSelectionController.ScrubPathAll();
                    if (Input.GetMouseButtonDown(0))
                    {
                        GridSelectionController.DeselectAll();
                        SelectedCharacter = null;
                    }
                    return;
                }
            }

            if (SelectedCharacter != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hitTile.Controller().OccupantCharacter == SelectedCharacter)
                    {
                        hitTile.Controller().Select();
                    } else
                    {
                        MoveCharacter(SelectedCharacter, hitTile);
                    }
                } else
                {
                    hitTile.Controller().HoverPathfinding();
                }
            } else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hitTile.Controller().Select();
                }
                else
                {
                    hitTile.Controller().Hover();
                }
            }
        } else
        {
            GridSelectionController.ScrubPathAll();
            if (Input.GetMouseButtonDown(0))
                GridSelectionController.DeselectAll();
        }
    }
        
    #region ICharacterSelectionController implementation

    #endregion

    #region ICharacterMovementController implementation

    public void MoveCharacter(ICharacter character, IHexTile endTile)
    {
        character.GetOccupiedTile().Controller().Deselect();
        character.GetOccupiedTile().Controller().OccupantCharacter = null;


        endTile.SetChild(character.GetGameObject());
        character.GetGameObject().transform.localPosition = new Vector3(0, 0, 0);

        endTile.Controller().OccupantCharacter = character;
        endTile.Controller().Select();
    }

    #endregion
}

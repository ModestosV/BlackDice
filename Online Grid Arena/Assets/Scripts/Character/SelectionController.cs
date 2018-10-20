using UnityEngine;
using System;

public class SelectionController : MonoBehaviour, IMonoBehaviour, ICharacterSelectionController, ICharacterMovementController
{
    [SerializeField] public StatPanel statPanel;

    public ICharacter SelectedCharacter { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }
    public IGridTraversalController GridTraversalController { get; set; }

    private void Start()
    {
        GridSelectionController = FindObjectOfType<Grid>().controller.GridSelectionController;
        GridTraversalController = FindObjectOfType<Grid>().controller.GridTraversalController;
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
                bool isHitTileReachable = GridTraversalController.GetPath(SelectedCharacter.GetOccupiedTile(), hitTile).Count != 0;

                if (Input.GetMouseButtonDown(0))
                {
                    if (hitTile.Controller().OccupantCharacter == SelectedCharacter)
                    {
                        hitTile.Controller().Select();
                    } else if (isHitTileReachable)
                    {
                        MoveCharacter(SelectedCharacter, hitTile);
                    }
                } else if (isHitTileReachable)
                {
                    hitTile.Controller().HoverPathfinding();
                } else
                {
                    hitTile.Controller().HoverError();
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
            {
                GridSelectionController.DeselectAll();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}

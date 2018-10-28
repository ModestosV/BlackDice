using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour, IGameManager
{
    public SelectionController selectionController;
    public GridSelectionController gridSelectionController;
    public GridTraversalController gridTraversalController;
    public TurnController turnController;

    #region IGameManager implementation

    public ISelectionController SelectionController
    {
        get { return selectionController; }
    }

    #endregion

    private void Awake()
    {
        selectionController.GridSelectionController = gridSelectionController;
        selectionController.GridTraversalController = gridTraversalController;
        selectionController.GameManager = this;
        selectionController.StatPanel = FindObjectOfType<StatPanel>();
        selectionController.StatPanel.Controller.DisableStatDisplays();
        selectionController.PlayerPanel = FindObjectOfType<PlayerPanel>();

        turnController.Init();
        Character[] charactersArray = FindObjectsOfType<Character>();
        List<ICharacter> charactersList = new List<ICharacter>();
        foreach (ICharacter character in charactersArray)
        {
            character.Controller.TurnController = turnController;
            charactersList.Add(character);
        }

        turnController.RefreshedCharacters = charactersList;

        selectionController.TurnController = turnController;


        FindObjectOfType<Grid>().Init(gridSelectionController, gridTraversalController);
    }

    private void Start()
    {
        turnController.StartNextTurn();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool isEscapeButtonDown = Input.GetKeyDown(KeyCode.Escape);
        bool mouseIsOverGrid = Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Tile";
        bool isLeftClickDown = Input.GetMouseButtonDown(0);

        IHexTile targetTile = null;
        if (mouseIsOverGrid)
        {
            targetTile = hit.collider.gameObject.GetComponent<HexTile>();
        }

        selectionController.IsEscapeButtonDown = isEscapeButtonDown;
        selectionController.MouseIsOverGrid = mouseIsOverGrid;
        selectionController.IsLeftClickDown = isLeftClickDown;
        selectionController.TargetTile = targetTile;

        selectionController.Update();
    }

    #region IGameManager implementation

    public void QuitApplication()
    {
        Application.Quit();
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}

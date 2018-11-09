using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TurnPanel : MonoBehaviour, ITurnPanel
{
    private TurnPanelController turnPanelController;

    public ITurnPanelController Controller { get { return turnPanelController; } }

    public GameObject GameObject
    {
        get { return gameObject; }
    }
  
    public List<GameObject> turnTiles;
    public List<string> players;

    private void Awake()
    {
        players = new List<string>();
        foreach (Character character in FindObjectsOfType<Character>())
        {
            if (!players.Contains(character.playerName))
                players.Add(character.playerName);
        }

        turnPanelController = new TurnPanelController()
        {
            TurnPanel = this,
            TurnTiles = new List<ITurnTileController>(),
            CharacterOrder = new List<ICharacterController>(),
            PlayerNames = players
        };

        turnTiles = new List<GameObject>();
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("TurnTile").OrderBy(go=>go.name))
        {
            turnPanelController.AddTurnTile(tile.GetComponent<TurnTile>().Controller);
            turnTiles.Add(tile);
        }
    }
    
}

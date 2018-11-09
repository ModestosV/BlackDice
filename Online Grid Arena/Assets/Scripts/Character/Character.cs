using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Character : MonoBehaviour, ICharacter
{
    public List<CharacterStat> stats;
    public List<Ability> abilities;
    public CharacterStatNameSet characterStatNameSet;
    public string playerName;

    private CharacterController characterController;

    [SerializeField]
    public Texture characterIcon;

    private void Awake()
    {
        characterController = new CharacterController
        {
            StatNames = characterStatNameSet.StatNames,
            CharacterStats = stats.ToList<ICharacterStat>(),
            Abilities = abilities.ToList<IAbility>(),
            Character = this,
            OwnedByPlayer = playerName,
            CharacterIcon = characterIcon
        };
    }

    private void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
    }

    #region ICharacter implementation

    public ICharacterController Controller { get { return characterController; } }

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.parent = targetTile.GameObject.transform;
        GameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    #endregion

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), characterController.ToString());
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}

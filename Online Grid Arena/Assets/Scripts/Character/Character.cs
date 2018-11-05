using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Character : MonoBehaviour, ICharacter
{
    private CharacterController controller;
    public List<CharacterStat> stats;
    public List<Ability> abilities;
    public CharacterStatNameSet characterStatNameSet;

    private void Awake()
    {
        controller = new CharacterController
        {
            StatNames = characterStatNameSet.StatNames,
            CharacterStats = stats.ToList<ICharacterStat>(),
            Abilities = abilities.ToList<IAbility>(),
            OccupiedTile = GetComponentInParent<HexTile>().Controller
        };
    }

    #region ICharacter implementation

    public ICharacterController Controller { get { return controller; } }

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.parent = targetTile.GameObject.transform;
        GameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    #endregion

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), controller.ToString());
    }

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
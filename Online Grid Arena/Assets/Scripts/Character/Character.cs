using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour, ICharacter
{
    // Public members to be instantiated by/through Unity
    public CharacterController controller;
    public List<CharacterStat> stats;
    public List<Ability> abilities;
    public CharacterStatNameSet characterStatNameSet;

    private void OnValidate()
    {
        InitStats();
        InitAbilities();
        controller.CharacterStatNameSet = characterStatNameSet;
    }

    private void Awake()
    {
        controller.Character = this;
        InitStats();
        InitAbilities();
        controller.CharacterStatNameSet = characterStatNameSet;
        controller.OccupiedTile = GetComponentInParent<HexTile>().Controller;
    }

    private void InitStats()
    {
        List<ICharacterStat> statList = new List<ICharacterStat>();
        foreach (CharacterStat stat in stats)
        {
            statList.Add(stat);
        }
        controller.CharacterStats = statList;
    }

    private void InitAbilities()
    {
        List<IAbility> abilityList = new List<IAbility>();
        foreach(Ability ability in abilities)
        {
            abilityList.Add(ability);
        }
        controller.Abilities = abilityList;
    }

    #region ICharacter implementation

    public ICharacterController Controller { get { return controller; } }

    public void MoveToTile(IHexTile targetTile)
    {
        targetTile.SetChild(GameObject);
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
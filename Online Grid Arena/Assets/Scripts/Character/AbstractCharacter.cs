﻿using UnityEngine;

public abstract class AbstractCharacter : BlackDiceMonoBehaviour, ICharacter
{
    [SerializeField] protected string playerName;

    protected CharacterController characterController;

    [SerializeField] protected Texture characterIcon;
    [SerializeField] protected Color32 borderColor;

    protected GameObject teamColorIndicator;
    protected GameObject activeCircle;
    protected GameObject healthBar;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public ICharacterController Controller { get { return characterController; } }

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.SetParent(targetTile.GameObject.transform);
        gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 0);
    }

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), characterController.ToString());
    }

    protected virtual void Awake()
    {
        activeCircle = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/ActiveCircle"), this.transform);
        activeCircle.transform.SetParent(this.transform);

        healthBar = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/HealthBar"), this.transform);
        healthBar.transform.SetParent(this.transform);

        teamColorIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/CharColorMarker"), this.transform);
        teamColorIndicator.transform.SetParent(this.transform);
        teamColorIndicator.GetComponent<SpriteRenderer>().color = borderColor;
    }

    void Start()
    {
        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
        characterController.ActiveCircle.isActive = false;
    }
}
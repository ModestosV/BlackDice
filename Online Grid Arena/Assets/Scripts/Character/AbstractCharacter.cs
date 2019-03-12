using UnityEngine;

public abstract class AbstractCharacter : BlackDiceMonoBehaviour, ICharacter, IEventSubscriber
{
    [SerializeField] protected string playerName;
    
    protected CharacterController characterController;

    [SerializeField] protected Texture characterIcon;
    [SerializeField] protected Color32 borderColor;

    protected GameObject teamColorIndicator;
    protected GameObject activeCircle;
    protected GameObject healthBar;
    protected GameObject exhausted;

    public void Destroy()
    {
        Destroy(gameObject);
        Debug.Log(ToString() + " has been removed from the game.");
    }

    public ICharacterController Controller { get { return characterController; } }

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.SetParent(targetTile.GameObject.transform);
        gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 0);
    }

    protected virtual void Awake()
    {
        Debug.Log(ToString() + " Awake() begin");

        activeCircle = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/ActiveCircle"), this.transform);
        activeCircle.transform.SetParent(this.transform);

        healthBar = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/HealthBar"), this.transform);
        healthBar.transform.SetParent(this.transform);

        teamColorIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/CharColorMarker"), this.transform);
        teamColorIndicator.transform.SetParent(this.transform);
        teamColorIndicator.GetComponent<SpriteRenderer>().color = borderColor;

        exhausted = Instantiate(transform.GetChild(0).gameObject, transform) as GameObject;
        exhausted.transform.localScale *= 1.02f;
        exhausted.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Shadowed") as Material;
        exhausted.SetActive(false);

        Debug.Log(ToString() + " Awake() end");
    }

    void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();
        characterController.UpdateHealthBar();
        characterController.ActiveCircle.enabled = false;

        Debug.Log(ToString() + " Start() end");
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(ExhaustCharacterEvent))
        {
            var exhaustCharacterEvent = (ExhaustCharacterEvent)@event;
            if (exhaustCharacterEvent.CharacterController == this.characterController)
            {
                exhausted.SetActive(true);
            }
        }
        else if (type == typeof(NewRoundEvent))
        {
            var newRoundEvent = (NewRoundEvent)@event;
            if (newRoundEvent.CharacterController == this.characterController)
            {
                exhausted.SetActive(true);
            }
        }
    }
}
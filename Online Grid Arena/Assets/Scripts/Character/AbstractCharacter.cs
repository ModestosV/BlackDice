using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCharacter : BlackDiceMonoBehaviour, ICharacter, IEventSubscriber
{
    [SerializeField] protected string playerName;

    protected CharacterController characterController;

    [SerializeField] protected Texture characterIcon;
    [SerializeField] protected Color32 borderColor;

    private GameObject teamColorIndicator;
    protected GameObject shield;
    protected GameObject exhausted;

    private GameObject indicator;
    private Animator iAnimator;
    protected List<IEffect> effects;

    private Material baseMaterial;
    [SerializeField]
    private Material exhaustedBlack;

    private float speed = 0.5f;
    private bool following = false;
    private int targetIndex;
    private Vector3 currentWaypoint;
    private List<IHexTileController> path;

    bool isIdle;
    bool isGettingHurt;
    bool isAttacking;
    bool isJumping;

    private void Update()
    {
        if (following)
        {
            if (gameObject.transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    following = false;
                }

                if (following)
                {
                    currentWaypoint = new Vector3(path[targetIndex].HexTile.GameObject.transform.position.x, transform.position.y, path[targetIndex].HexTile.GameObject.transform.position.z);
                }
            }
            if (following)
            {
                transform.LookAt(currentWaypoint);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentWaypoint, speed);
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Debug.Log(ToString() + " has been removed from the game.");
    }

    public ICharacterController Controller => characterController;

    public void MoveToTile(IHexTile targetTile)
    {
        gameObject.transform.SetParent(targetTile.GameObject.transform);
        gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 0);
    }

    public void FollowPath(List<IHexTileController> path, IHexTile targetTile)
    {
        this.path = path;
        targetIndex = 0;
        gameObject.transform.SetParent(targetTile.GameObject.transform);
        currentWaypoint = path[0].HexTile.GameObject.transform.position;
        following = true;
    }

    protected Dictionary<string, ICharacterStat> InitializeStats(int health, int moves, int attack, int defense)
    {
        ICharacterStat Health = new CharacterStat(health);
        ICharacterStat Moves = new CharacterStat(moves);
        ICharacterStat Attack = new CharacterStat(attack);
        ICharacterStat Defense = new CharacterStat(defense);

        return new Dictionary<string, ICharacterStat>()
        {
            { "health", Health },
            { "moves", Moves },
            { "attack", Attack },
            { "defense", Defense }
        };
    }

    protected virtual void Awake()
    {
        effects = new List<IEffect>();

        Debug.Log(ToString() + " Awake() begin");
        teamColorIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/CharColorMarker"), this.transform);
        teamColorIndicator.transform.SetParent(this.transform);
        teamColorIndicator.GetComponent<SpriteRenderer>().color = borderColor;

        shield = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/Shield"), this.transform);
        shield.transform.SetParent(this.transform);

        indicator = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/CharacterIndicator"), this.transform);
        indicator.transform.SetParent(this.transform);
        iAnimator = indicator.GetComponent<Animator>();
        
        Debug.Log(ToString() + " Awake() end");
    }

    void Start()
    {
        Debug.Log(ToString() + " Start() begin");

        GetComponentInParent<HexTile>().Controller.OccupantCharacter = characterController;
        characterController.RefreshStats();

        if (transform.GetChild(0).gameObject.GetComponent<Renderer>() != null)
        {
            baseMaterial = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        }
        else
        {
            baseMaterial = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        }

        Debug.Log(ToString() + " Start() end");
    }

    public void Handle(IEvent @event)
    {
        if (characterController.CharacterState != CharacterState.DEAD)
        {
            var type = @event.GetType();
            if (type == typeof(ExhaustCharacterEvent))
            {
                var exhaustCharacterEvent = (ExhaustCharacterEvent)@event;
                if (exhaustCharacterEvent.CharacterController == this.characterController)
                {
                    if (transform.GetChild(0).gameObject.GetComponent<Renderer>() != null)
                    {
                        transform.GetChild(0).gameObject.GetComponent<Renderer>().material = exhaustedBlack;
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = exhaustedBlack;
                    }
                }
            }
            else if (type == typeof(NewRoundEvent))
            {
                var newRoundEvent = (NewRoundEvent)@event;
                if (newRoundEvent.CharacterController == this.characterController)
                {
                    if (transform.GetChild(0).gameObject.GetComponent<Renderer>() != null)
                    {
                        transform.GetChild(0).gameObject.GetComponent<Renderer>().material = baseMaterial;
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = baseMaterial;
                    }
                }
            }
            else if (type == typeof(SelectActivePlayerEvent))
            {
                var selectActivePlayerEvent = (SelectActivePlayerEvent)@event;
                if (selectActivePlayerEvent.ActivePlayer.Name == this.characterController.Owner
                    && characterController.CharacterState != CharacterState.EXHAUSTED)
                {
                    iAnimator.SetBool("Selectable", true);
                    iAnimator.SetBool("Active", false);
                }
                else
                {
                    iAnimator.SetBool("Selectable", false);
                    iAnimator.SetBool("Active", false);
                }
            }
            else if (type == typeof(SelectCharacterEvent))
            {
                var selectCharacterEvent = (SelectCharacterEvent)@event;
                if (selectCharacterEvent.Character == this.characterController)
                {
                    iAnimator.SetBool("Active", true);
                    iAnimator.SetBool("Selectable", false);
                }
                else
                {
                    iAnimator.SetBool("Active", false);
                    iAnimator.SetBool("Selectable", false);
                }
            }
        }
    }
}

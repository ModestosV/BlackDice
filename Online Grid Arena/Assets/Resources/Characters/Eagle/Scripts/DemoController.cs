using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DemoController : MonoBehaviour
{
	private Animator animator;

	public float walkspeed = 5;
	private float horizontal;
	private float vertical;
	private readonly float rotationDegreePerSecond = 1000;
	private bool isAttacking;

	public GameObject gamecam;
	public Vector2 camPosition;
	private bool dead;


	public GameObject[] characters;
	public int currentChar;

    public GameObject[] targets;
    public float minAttackDistance;

    public Text nameText;


	void Start()
	{
		SetCharacter(0);
	}

	void FixedUpdate()
	{
		if (animator && !dead)
		{
			//walk
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");

			Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
			float speedOut;

			if (stickDirection.sqrMagnitude > 1) stickDirection.Normalize();

			if (!isAttacking)
				speedOut = stickDirection.sqrMagnitude;
			else
				speedOut = 0;

			if (stickDirection != Vector3.zero && !isAttacking)
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(stickDirection, Vector3.up), rotationDegreePerSecond * Time.deltaTime);
			GetComponent<Rigidbody>().velocity = transform.forward * speedOut * walkspeed + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

			animator.SetFloat("Speed", speedOut);
		}
	}

	void Update()
	{
		if (!dead)
		{
			// move camera
			if (gamecam)
				gamecam.transform.position = transform.position + new Vector3(0, camPosition.x, -camPosition.y);

			// attack
			if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") && !isAttacking)
			{
				isAttacking = true;
				animator.SetTrigger("Attack");
				StartCoroutine(StopAttack(1));
                TryDamageTarget();


            }
            // get Hit
            if (Input.GetKeyDown(KeyCode.N) && !isAttacking)
            {
                isAttacking = true;
                animator.SetTrigger("Hit");
                StartCoroutine(StopAttack(1));
            }

            animator.SetBool("isAttacking", isAttacking);

			//switch character

			if (Input.GetKeyDown("left"))
			{
				SetCharacter(-1);
				isAttacking = true;
				StartCoroutine(StopAttack(1f));
			}

			if (Input.GetKeyDown("right"))
			{
				SetCharacter(1);
				isAttacking = true;
				StartCoroutine(StopAttack(1f));
			}

			// death
			if (Input.GetKeyDown("m"))
				StartCoroutine(Selfdestruct());

            //Leave
            if (Input.GetKeyDown("l"))
            {
                if (ContainsParam(animator,"Leave"))
                {
                    animator.SetTrigger("Leave");
                    StartCoroutine(StopAttack(1f));
                }
            }
        }

	}
    GameObject target;

    private void TryDamageTarget()
    {
        target = null;
        float targetDistance = minAttackDistance + 1;
        foreach (var item in targets)
        {
            float itemDistance = (item.transform.position - transform.position).magnitude;
            if (itemDistance < minAttackDistance)
            {
                if (target == null) {
                    target = item;
                    targetDistance = itemDistance;
                }
                else if (itemDistance < targetDistance)
                {
                    target = item;
                    targetDistance = itemDistance;
                }
            }
        }
        if(target != null)
        {
            transform.LookAt(target.transform);
            
        }
    }
    public void DealDamage(DealDamageComponent comp)
    {
        if (target == null) return;
        target.GetComponent<Animator>().SetTrigger("Hit");
        var hitFX = Instantiate(comp.hitFX);
        hitFX.transform.position = target.transform.position + new Vector3(0, target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center.y,0);
    }

    private IEnumerator StopAttack(float length)
	{
		yield return new WaitForSeconds(length); 
		isAttacking = false;
	}

    private IEnumerator Selfdestruct()
    {
        animator.SetTrigger("isDead");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        dead = true;

        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (Input.anyKeyDown)
            {
                Application.LoadLevel(Application.loadedLevelName);
                yield break;
            }

            yield return 0;

        }
    }

    private void SetCharacter(int i)
	{
		currentChar += i;

		if (currentChar > characters.Length - 1)
			currentChar = 0;
		if (currentChar < 0)
			currentChar = characters.Length - 1;

		foreach (GameObject child in characters)
		{
            if (child == characters[currentChar])
            {
                child.SetActive(true);
                if (nameText != null)
                    nameText.text = child.name;
            }
            else
            {
                child.SetActive(false);
            }
		}
		animator = GetComponentInChildren<Animator>();
    }

    private bool ContainsParam(Animator anim, string paramName)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }
}

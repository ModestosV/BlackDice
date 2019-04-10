using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    float timer;
    public float deathtimer = 1;
    public GameObject OnDestroyFuncTarget;
    public string OnDestroyFuncMessage;

    // Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= deathtimer)
        {
            if (OnDestroyFuncTarget != null)
                OnDestroyFuncTarget.SendMessage(OnDestroyFuncMessage);
            Destroy(gameObject);
        }
	
	}
}

/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;

public class Character : MonoBehaviour, IMovementController
{
    public CharacterController controller;

    public void MoveX(float value)
    {
        float deltaX = Time.fixedDeltaTime * value;
        transform.Translate(deltaX, 0, 0);

        Debug.Log(string.Format("Character \"{0}\" has moved {1} on X axis to position {2}.", this.name, deltaX, transform.position));
    }

    public void MoveY(float value)
    {
        float deltaY = Time.fixedDeltaTime * value;
        transform.Translate(0, deltaY, 0);

        Debug.Log(string.Format("Character \"{0}\" has moved {1} on Y axis to position {2}.", this.name, deltaY, transform.position));
    }

    private void OnEnable()
    {
        controller.SetMovementController(this);
    }

    private void FixedUpdate()
    {
        if(Input.GetButton("Horizontal"))
            controller.MoveX(Input.GetAxis("Horizontal"));
        if (Input.GetButton("Vertical"))
            controller.MoveY(Input.GetAxis("Vertical"));
    }

    public override string ToString()
    {
        return string.Format("(Character|{0}: {1})", this.GetHashCode(), controller.ToString());
    }
}

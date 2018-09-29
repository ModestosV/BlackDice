/*
 * Credit: Kryzarel's free Unity asset titled "Character Stats".
 * Obtained from Unity Asset Store on 2018/09/14. https://assetstore.unity.com/packages/tools/integration/character-stats-106351
 */

using UnityEngine;

public class Character : MonoBehaviour, IMovementController
{
    public CharacterController controller;

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

    #region IMovementController implementation

    public void MoveX (float value)
    {
        float deltaX = Time.fixedDeltaTime * value;
        transform.Translate(deltaX, 0, 0);
    }

    public void MoveY (float value)
    {
        float deltaY = Time.fixedDeltaTime * value;
        transform.Translate(0, deltaY, 0);
    }

    #endregion
}

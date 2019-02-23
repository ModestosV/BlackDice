using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    private List<ICharacterController> characterControllers;

    public Player()
    {
        Debug.Log("Player has been initialized");
        characterControllers = new List<ICharacterController>();
    }

    public void AddCharacterController(ICharacterController characterController)
    {
        characterControllers.Add(characterController);
    }

    public void RefreshCharacters()
    {
        foreach (ICharacterController characterController in characterControllers)
        {
            if (characterController.CharacterState != CharacterState.DEAD)
            {
                characterController.CharacterState = CharacterState.UNUSED;
            }
        }
    }

    public List<ICharacterController> GetUnusedCharacters()
    {
        var unusedCharacters = new List<ICharacterController>();
        foreach (ICharacterController characterController in characterControllers)
        {
            if (characterController.CharacterState == CharacterState.UNUSED)
            {
                unusedCharacters.Add(characterController);
            }
        }
        return unusedCharacters;
    }
}
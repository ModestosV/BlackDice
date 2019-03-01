using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    public List<ICharacterController> CharacterControllers { get; }

    public Player()
    {
        Debug.Log("Player has been initialized");
        CharacterControllers = new List<ICharacterController>();
    }

    public void AddCharacterController(ICharacterController characterController)
    {
        CharacterControllers.Add(characterController);
    }

    public void RefreshCharacters()
    {
        foreach (ICharacterController characterController in CharacterControllers)
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
        foreach (ICharacterController characterController in CharacterControllers)
        {
            if (characterController.CharacterState == CharacterState.UNUSED)
            {
                unusedCharacters.Add(characterController);
            }
        }
        return unusedCharacters;
    }
}
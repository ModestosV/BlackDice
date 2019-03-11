using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    public List<ICharacterController> CharacterControllers { get; }
    public string Name { get; }

    public Player(string name)
    {
        Name = name;
        Debug.Log($"Player with name {Name} has been initialized");
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

    public bool AreAllCharactersDead()
    {
        var aliveCharacters = new List<ICharacterController>();
        foreach (ICharacterController characterController in CharacterControllers)
        {
            if (characterController.CharacterState != CharacterState.DEAD)
            {
                aliveCharacters.Add(characterController);
            }
        }

        Debug.Log($"{Name} has {aliveCharacters.Count} characters alive");

        return aliveCharacters.Count == 0;
    }

    public override string ToString()
    {
        return Name;
    }
}
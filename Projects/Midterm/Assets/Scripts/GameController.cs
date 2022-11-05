using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialogue }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameState state;

    private void Start() //basically locks player from doing other things (i.e moving) once dialogue goes off
    {
        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.Dialogue;
        };

        DialogueManager.Instance.OnCloseDialogue += () => //dialogue ended/player can do stuff again
        {
            if (state == GameState.Dialogue)
            state = GameState.FreeRoam;
        };
    }

    private void Update() //calls to other codes
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] private AudioSource dialogueSfx;

    public void Interact() //initiate dialogue upon interaction
    {
        dialogueSfx.Play();
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
}

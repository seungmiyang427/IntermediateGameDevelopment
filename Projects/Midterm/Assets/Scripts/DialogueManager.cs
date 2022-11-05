using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialogue(Dialogue dialogue) //dialogue setup + first line of dialogue in box
    {
        yield return new WaitForEndOfFrame();

        OnShowDialogue?.Invoke();

        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        MusicController.i.PlaySfx(AudioId.Dialogue); //play sound effect
        Debug.Log("Click!");
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public void HandleUpdate() //next dialogue
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            MusicController.i.PlaySfx(AudioId.Dialogue); //play sound effect
            Debug.Log("Click!");
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogueBox.SetActive(false);
                OnCloseDialogue?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialogue(string line) //dialogue animation
    {
        isTyping = true;
        dialogueText.text = ""; //setting dialogue as empty string
        foreach (var letter in line.ToCharArray()) //looping through each letter, making them show up one by one
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}

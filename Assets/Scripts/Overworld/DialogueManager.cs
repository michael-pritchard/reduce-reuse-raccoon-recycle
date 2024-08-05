using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialogueBox;

    public void ShowDialogue(string dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
    }
}

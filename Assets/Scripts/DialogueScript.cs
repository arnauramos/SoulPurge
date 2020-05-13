using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject TextBox;
    public string dialogue;
    private bool dialogueFinished = false;
    private bool playingDialogue = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogueFinished)
        {
            closeDialogue();
        }
    }

    public void playDialogue()
    {
        dialogueFinished = false;
        PlayerManager.Instance.playerDisabled = true;
        TextBox.SetActive(true);
        if (!playingDialogue)
        {
            playingDialogue = true;
            StopAllCoroutines();
            StartCoroutine(sayText());
        }
    }

    private void closeDialogue()
    {
        Text.text = " ";
        TextBox.SetActive(false);
        PlayerManager.Instance.usePriority = false;
        PlayerManager.Instance.playerDisabled = false;
        dialogueFinished = false;
    }

    IEnumerator sayText()
    {
        Text.text = " ";
        for (int x = 0; x < dialogue.Length; x++)
        {
            Text.text += dialogue[x];
            yield return null;
        }
        dialogueFinished = true;
        playingDialogue = false;
    }
}

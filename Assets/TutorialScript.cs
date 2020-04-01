using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class TutorialScript : MonoBehaviour
{
    public List<string> Dialogue;
    public List<TextMeshProUGUI> TextArea;
    public List<bool> DialogueFinished;
    public enum DialogueState { NONE, DIALOGUE1, DIALOGUE2, FINISHED}
    public DialogueState dialogue = DialogueState.DIALOGUE1;
    private bool dialogueActive = false;
    public int currentDialogue = -1;
    // Update is called once per frame
    private void Start()
    {
        if (PlayerManager.Instance.tutorialDone)
        {
            Destroy(gameObject);
        }
        // INIT BOOL VECTOR
        for (int i = 0; i < Dialogue.Capacity; i++)
        {
            DialogueFinished.Add(false);
        }
    }
    void Update()
    {
        ShowDialogue();
        switch (dialogue)
        {
            case DialogueState.NONE:
                currentDialogue = -1;
                break;
            case DialogueState.DIALOGUE1:
                SayDialogue(0);
                if (((Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Vertical") > 0.2f)) && DialogueFinished[0])
                {
                    dialogue = DialogueState.DIALOGUE2;
                }
                break;
            case DialogueState.DIALOGUE2:
                SayDialogue(1);
                if (Input.GetKey(KeyCode.E))
                {
                    if (PlayerManager.Instance.playerDisabled)
                    {
                        dialogue = DialogueState.FINISHED;
                    }
                }
                break;
            case DialogueState.FINISHED:
                currentDialogue = -1;
                FinishedTutorial();
                break;
            default:
                break;
        }
    }

    private void FinishedTutorial()
    {
        // SAVE TUTORIAL DONE IN FILE
        BinaryWriter writer = new BinaryWriter(File.Open("tutorial.bin", FileMode.Create));
        writer.Write(1);
        // SAVE TUTORIAL DONE IN MANAGER
        PlayerManager.Instance.tutorialDone = true;
        // DESTROY THIS
        Destroy(gameObject);
    }

    private void ShowDialogue()
    {
        if (currentDialogue < 0)
        {
            if (TextArea.Capacity > 0)
            {
                for (int i = 0; i < TextArea.Capacity; i++)
                {
                    TextArea[i].enabled = false;
                }
            }
            return;
        }
        for (int i = 0; i < TextArea.Capacity; i++)
        {
            if (i == currentDialogue)
            {
                TextArea[i].enabled = true;
            }
            else
            {
                TextArea[i].enabled = false;
            }
        }
    }
    private void SayDialogue(int i)
    {
        if (!dialogueActive && !DialogueFinished[i])
        {
            currentDialogue = i;
            dialogueActive = true;
            StopAllCoroutines();
            StartCoroutine(sayText(i));
        }
    }

    IEnumerator sayText(int i)
    {
        TextArea[i].text = " ";
        for (int x = 0; x < Dialogue[i].Length; x++)
        {
            TextArea[i].text += Dialogue[i][x];
            yield return null;
        }
        DialogueFinished[i] = true;
        dialogueActive = false;
    }
}

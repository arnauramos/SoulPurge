using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    enum Dialogue { NONE, WELCOME, MOVE, SHOOT, SOULS, READY}
    Dialogue dialogue = Dialogue.NONE;
    public TextMeshProUGUI Text;
    private bool dialogueDone = false;
    private bool dialogueFinished = false;
    void Start()
    {
        Text = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        switch (dialogue)
        {
            case Dialogue.NONE:
                dialogue = Dialogue.WELCOME;
                break;
            case Dialogue.WELCOME:
                if (!dialogueDone)
                {
                    StopAllCoroutines();
                    StartCoroutine(sayWelcome());
                    dialogueDone = true;
                }
                if (Input.GetKey(KeyCode.Return) && dialogueFinished)
                {
                    dialogueDone = false;
                    dialogueFinished = false;
                    dialogue = Dialogue.MOVE;
                }
                break;
            case Dialogue.MOVE:
                if (!dialogueDone)
                {
                    StopAllCoroutines();
                    StartCoroutine(sayMove());
                    dialogueDone = true;
                }
                if ((Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Vertical") > 0.2f) && dialogueDone)
                {
                    dialogueDone = false;
                    dialogueFinished = false;
                    dialogue = Dialogue.SHOOT;
                }
                break;
            case Dialogue.SHOOT:
                if (!dialogueDone)
                {
                    StopAllCoroutines();
                    StartCoroutine(sayShoot());
                    dialogueDone = true;
                }
                if (Input.GetMouseButton(0) && dialogueFinished)
                {
                    dialogueDone = false;
                    dialogueFinished = false;
                    dialogue = Dialogue.SOULS;
                }
                break;
            case Dialogue.SOULS:
                if (!dialogueDone)
                {
                    StopAllCoroutines();
                    StartCoroutine(saySouls());
                    dialogueDone = true;
                }
                if (Input.GetKey(KeyCode.Return) && dialogueFinished)
                {
                    dialogueDone = false;
                    dialogueFinished = false;
                    dialogue = Dialogue.READY;
                }
                break;
            case Dialogue.READY:
                if (!dialogueDone)
                {
                    StopAllCoroutines();
                    StartCoroutine(sayReady());
                    dialogueDone = true;
                }
                if (Input.GetKey(KeyCode.Return) && dialogueFinished)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
    IEnumerator sayWelcome()
    {
        string say = "Welcome to the safe zone purger, we need your help to end with all the demons in the hostile zone.";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
        dialogueFinished = true;
    }
    IEnumerator sayMove()
    {
        string say = "Press WASD  or the direction keys to move.";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
        dialogueFinished = true;
    }

   IEnumerator sayShoot()
    {
        string say = "Use the mouse to aim and press the left mouse button to shoot";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
        dialogueFinished = true;
    }
    IEnumerator saySouls()
    {
        string say = "Demons drop their souls when you kill them. In the safe zone you can sell all the souls you pick up for money. The more money you have, the better weapons and objects you can buy!";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
        dialogueFinished = true;
    }
    IEnumerator sayReady()
    {
        string say = "You are ready to go into the hostile zone, good luck!";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
        dialogueFinished = true;
    }
}

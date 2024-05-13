using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : Singleton<DialogueManager>
{
    public PlayerController player;
    public DialogueRunner dialogueRunner;

    public bool isDialoguing;

    public void StartDialogue(string startNode)
    {
        isDialoguing = true;
        dialogueRunner.StartDialogue(startNode);
        player.LockPlayer();
    }
}

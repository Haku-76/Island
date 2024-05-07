using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance{get => _instance;}


    void Awake()
    {
        if(_instance != null)
        {
            Destroy(_instance);
        }
        else
        {
            _instance = this;
        }
    }

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

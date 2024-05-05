using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GuideManController : NPC 
{
    void Start()
    {
        DialogueManager.Instance.StartDialogue("GuideDialogue");
    }
}


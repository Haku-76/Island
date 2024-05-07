using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GuideManController : NPC
{
    public bool isTimeGuideTriggered;
    void Start()
    {
        isTimeGuideTriggered = false;
        DialogueManager.Instance.StartDialogue("GuideDialogue");
    }

    void TimeGuideStart()
    {
        DialogueManager.Instance.StartDialogue("TimeGuide");
    }
}


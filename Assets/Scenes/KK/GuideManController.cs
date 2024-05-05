using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GuideManController : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner.StartDialogue("GuideDialogue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankSpanker : NPC
{   
    [Header("PlankSpanker_Pos")]
    public Vector3 workPos;

    public void PlayAni()
    {
        Debug.Log("PlayRecital");
        spAnimation = false;
        anim.SetBool("PlayRecital", true);
    }

    public void StopPlayAni()
    {
        Debug.Log("StopPlayRecital");
        // anim.SetBool("EventAnimation", false);
        anim.SetBool("PlayRecital", false);
    }

    public void StartWork()
    {
        transform.position = workPos;
        anim.SetBool("PlayRecital", true);
    }
}

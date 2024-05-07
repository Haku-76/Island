using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Novelist : NPC
{
    public Material mat2;

    protected override void SetMatAlpha(float target)
    {
        mat.SetFloat("_Alpha", target);
        mat2.SetFloat("_Alpha", target);
    }
}

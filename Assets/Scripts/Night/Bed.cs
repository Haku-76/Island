using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : TimeChange
{
    public override void Update()
    {
        if (TimeEventSystem.instance.timeQuantum==TimeQuantum.DayTime)
        {
            base.Update();
        }
    }
}

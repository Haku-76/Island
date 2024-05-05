using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : TimeChange
{
    public override void Update()
    {
        if (TimeEventSystem.instance.timeQuantum == TimeQuantum.WeekHours)
        {
            base.Update();
        }
    }
}

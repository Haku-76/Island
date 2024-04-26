using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventSystem : MonoBehaviour
{
    //public static TimeEventSystem instance;

    public static Action onTimeChange;

    public static int Day { get; private set; }
    public static int Month { get; private set; }
    public static TimeQuantum timeQuantum { get; private set; }
    
    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }*/

    public void Start()
    {
        Day = 4;
        Month = 7;
        timeQuantum = TimeQuantum.DayTime;
    }

    public void Skip()
    {
        timeQuantum++;
        if (timeQuantum == TimeQuantum.DayTime)
        {
            Day++;
        }
        if (Day > 30)
        {
            Month++;
        }
    }

    public void Update()
    {
        onTimeChange?.Invoke();
    }
}

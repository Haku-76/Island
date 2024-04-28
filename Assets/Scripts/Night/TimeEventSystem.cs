using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventSystem : MonoBehaviour
{
    private static TimeEventSystem _instance;
    public static TimeEventSystem instance{get => _instance;}

    public static event Action<int, int, TimeQuantum> onTimeChange;
    public static event Action<int, int, TimeQuantum> onLastTimeEnd;

    public int Day { get; private set; }
    public int Month { get; private set; }
    public TimeQuantum timeQuantum { get; private set; }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        Day = 30;
        Month = 6;
        timeQuantum = TimeQuantum.DayTime;
        onTimeChange?.Invoke(Month, Day, timeQuantum);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Skip();
        }
    }

    private void OnDisable()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
    [ContextMenu("SkipTime")]
    public void Skip()
    {
        onLastTimeEnd?.Invoke(Month, Day, timeQuantum);
        timeQuantum++;
        if ((int)timeQuantum == 3)
        {
            Day++;
            timeQuantum = TimeQuantum.DayTime;
        }
        if (Day > 30)
        {
            Month++;
            Day = 1;
        }
        onTimeChange.Invoke(Month, Day, timeQuantum);
    }

}
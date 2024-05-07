using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventSystem : MonoBehaviour
{
    private static TimeEventSystem _instance;
    public static TimeEventSystem instance{get => _instance;}

/// <summary>
/// 时间更新时触发 
/// </summary>
    public static event Action onTimeChanging;

/// <summary>
/// 时间更新完成后触发（更新过后的时间 Month, Day, TimeQuantum)
/// </summary>
    public static event Action<int, int, TimeQuantum> onTimeChange;
/// <summary>
/// 时间更新前触发 （更新前的时间 Month, Day, TimeQuantum)
/// </summary>
    public static event Action<int, int, TimeQuantum> onLastTimeEnd;

    public int Day { get; private set; }
    public int Month { get; private set; }
    public TimeQuantum timeQuantum { get; set; }

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

    public void OnEnable()
    {
        Day = 30;
        Month = 6;
        timeQuantum = TimeQuantum.DayTime;
    }

    void Start()
    {
        onTimeChange?.Invoke(Month, Day, timeQuantum);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
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

    private void SkipTime()
    {
        onTimeChanging?.Invoke();
        switch (timeQuantum)
        {
            case TimeQuantum.DayTime:
                timeQuantum = TimeQuantum.WeekHours;
                break;
            case TimeQuantum.WeekHours:
                timeQuantum = TimeQuantum.DayTime;
                Day++;
                if (Day > 30)
                {
                    Month++;
                    Day = 1;
                }
                break;
        }
    }

    IEnumerator SkipRoutine()
    {
        onLastTimeEnd?.Invoke(Month, Day, timeQuantum);
        TransitionManager.Instance.StartSkipTime();
        yield return new WaitForSeconds(1.5f);
        SkipTime();
        yield return new WaitForSeconds(3f);
        TransitionManager.Instance.EndSkipTime();
        onTimeChange?.Invoke(Month, Day, timeQuantum);

    }

    public void Skip()
    {
        StartCoroutine(SkipRoutine());
    }
}
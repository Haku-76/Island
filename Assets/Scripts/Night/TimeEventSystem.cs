using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeQuantum
{
    DayTime,
    Dusk,
    WeekHours
}

public class TimeEventSystem : MonoBehaviour
{
    public static TimeEventSystem instance;
    public event Action<TimeEvent> onTimeChange;
    List<TimeEvent> daytime = new List<TimeEvent>();

    private void Awake()
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
        AddTimeEvents();
        DontDestroyOnLoad(gameObject);
    }

    public void Skip()
    {

    }

    public void TimeChange(TimeEvent cur)
    {
        if (onTimeChange!=null)
        {
            onTimeChange(cur);
        }
    }

    private void AddTimeEvents()
    {
     
        for (int day = 1; day <= 31; day++)
        {
            // 添加 DayTime 时间事件
            daytime.Add(new TimeEvent(7, day, TimeQuantum.DayTime));

            // 添加 Dusk 时间事件
            daytime.Add(new TimeEvent(7, day, TimeQuantum.Dusk));

            // 添加 WeekHours 时间事件
            daytime.Add(new TimeEvent(7, day, TimeQuantum.WeekHours));
        }
    }

}

public class TimeEvent : MonoBehaviour
{
    int Month;
    int Day;
    TimeQuantum timeQuantum;

    public TimeEvent(int month, int day, TimeQuantum time)
    {
        this.Month = month;
        this.Day = day;
        this.timeQuantum = time;
    }
}

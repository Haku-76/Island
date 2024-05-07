using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScheduleDetails : IComparable<ScheduleDetails>
{
    public int month;
    public int day;
    public TimeQuantum time;

    public int priority;

    public Vector3 burnPosition;
    public Vector3 targetPosition;

    public AnimationClip clipAfterArive;
    public string dialogueStartNode;
    

    public ScheduleDetails(int day, TimeQuantum time, int priority, Vector3 burnPosition, Vector3 targetPosition,string dialogueStartNode)
    {
        this.day = day;
        this.time = time;
        this.priority = priority;
        this.burnPosition = burnPosition;
        this.targetPosition = targetPosition;
        this.dialogueStartNode = dialogueStartNode;
    }
    public int Time => month * 10000 + day * 100 + (int)time;
    public int CompareTo(ScheduleDetails other)
    {
        if(Time == other.Time)
        {
            if(priority > other.priority)
                return 1;
            else
                return -1;
        }
        else if(Time > other.Time)
            return 1;
        else if(Time < other.Time)
            return -1;
        
        return 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScheduleDetails : IComparable<ScheduleDetails>
{
    public int day;
    public TimeQuantum time;

    public int priority;

    public Vector3 burnPosition;
    public Vector3 targetPosition;

    //TODO:对话匣
    public string dialogueStartNode;

    public bool interactable;

    

    public ScheduleDetails(int day, TimeQuantum time, int priority, Vector3 burnPosition, Vector3 targetPosition,string dialogueStartNode, bool interactable)
    {
        this.day = day;
        this.time = time;
        this.priority = priority;
        this.burnPosition = burnPosition;
        this.targetPosition = targetPosition;
        this.dialogueStartNode = dialogueStartNode;
        this.interactable = interactable;
    }
    public int Time => day * 100 + (int)time;
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

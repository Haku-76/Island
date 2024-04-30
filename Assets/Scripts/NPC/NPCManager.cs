using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private static NPCManager _instance;
    public static NPCManager Instance{get => _instance;}
    private Dictionary<NPCCourse, bool> npcCourseList = new();

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(_instance);
        }
        else
        {
            _instance = this;
        }

        NPCCourse[] courses = (NPCCourse[])Enum.GetValues(typeof(NPCCourse));

        foreach(var npc in courses)
        {
            npcCourseList.Add(npc, true);
        }
    }
    
    public void SetNPCCourseOver(NPCCourse name)
    {
        npcCourseList[name] = false;
    }
    public bool GetNPCCourse(NPCCourse name)
    {
        return npcCourseList[name];
    }

}

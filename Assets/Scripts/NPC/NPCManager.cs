using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    private Dictionary<NPCCourse, bool> npcCourseList = new();
    private Dictionary<string, NPC> npcdic = new();
    public List<NPC> npcList = new();

    private void Awake()
    {
        base.Awake();
        NPCCourse[] courses = (NPCCourse[])Enum.GetValues(typeof(NPCCourse));

        foreach(var npc in courses)
        {
            npcCourseList.Add(npc, true);
        }

    }

    void Start()
    {
        foreach(var npc in npcList)
        {
            npcdic.Add(npc.GetType().Name, npc);
            npc.SetNPCActive(false);
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

    public void NPCEnter(string name, int flag1, int flag2)
    {
        var npc = npcdic[name];
        Debug.Log(npc.name);
        npc.NPCEnter(flag1, flag2, TimeQuantum.DayTime);
    }

    public void NPCExit(string name)
    {
        var npc = npcdic[name];
        npc.exitBar();
    }
}

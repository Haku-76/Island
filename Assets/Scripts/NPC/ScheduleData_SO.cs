using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScheduleData", menuName = "Schedule/ScheduleData")]
public class ScheduleData_SO : ScriptableObject
{
    public List<ScheduleDetails> scheduleList;
}

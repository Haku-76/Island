using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MailSchedule
{
    static MailSchedule()
    {
        mailSchedule = new();
        mailsOnGround = new();
    }
    public static List<MailScheduleDetails> mailSchedule;
    public static List<MailScheduleDetails> mailsOnGround;
}

public struct MailScheduleDetails
{
    public int month;
    public int day;
    public string letter_name;
}
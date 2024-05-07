using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : Singleton<MailManager>
{
    public ClickDetection clickDetection;
    public GameObject letter_Pre;

    void OnEnable()
    {
        TimeEventSystem.onTimeChange += OnTimeChangeEvent;
    }

    void OnDisable()
    {
        TimeEventSystem.onTimeChange -= OnTimeChangeEvent;
    }
    
    
    public void SendLetter(int afterDay, string letter_name)
    {
        MailScheduleDetails newSchedule = new MailScheduleDetails();
        newSchedule.month = TimeEventSystem.instance.Month;
        newSchedule.day = TimeEventSystem.instance.Day + afterDay;
        newSchedule.letter_name = letter_name;

        MailSchedule.mailSchedule.Add(newSchedule);
    }

    public void LeftLetter(string letter_name)
    {
        MailScheduleDetails newSchedule = new MailScheduleDetails();
        newSchedule.month = TimeEventSystem.instance.Month;
        newSchedule.day = TimeEventSystem.instance.Day + 1;
        newSchedule.letter_name = letter_name;
        MailSchedule.mailsOnGround.Add(newSchedule);
    }

    [ContextMenu("TestSendLetter")]
    public void TestSendLetter()
    {
        SendLetter(1, "Guitarist_3");
    }

    private void OnTimeChangeEvent(int month, int day, TimeQuantum timeQuantum)
    {
        Debug.Log("MailManager.OnTimeChangeEvent has been invoked");
        if(timeQuantum == TimeQuantum.DayTime)
        {
            var time_val = month * 1000 + day;
            
            foreach(var letter in MailSchedule.mailSchedule)
            {
                var cur_time = letter.month * 1000 + letter.day;
                if(time_val == cur_time)
                {
                    clickDetection.AddMail(letter.letter_name);
                    break;
                }
            }

            foreach(var letter in MailSchedule.mailsOnGround)
            {
                var cur_time = letter.month * 1000 + letter.day;
                if(time_val == cur_time)
                {
                    var _letter = Resources.Load<Item>(letter.letter_name);
                    var letter_obj = Instantiate(letter_Pre, transform);
                    letter_obj.transform.GetComponent<Mail>().letter = _letter;
                }
            }
        }
    }
}

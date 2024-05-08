using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text timeText;
    public GameObject WEK;
    public GameObject DAY;
    private void OnEnable()
    {
        TimeEventSystem.onTimeChange += UpDateTimeUI;
    }

    private void OnDisable()
    {
        TimeEventSystem.onTimeChange -= UpDateTimeUI;
    }

    private void UpDateTimeUI(int Day,int Month,TimeQuantum timeQuantum)
    {
        if (TimeEventSystem.instance.timeQuantum == TimeQuantum.DayTime)
        {
            DAY.SetActive(true);
            WEK.SetActive(false);
        }
        else if(TimeEventSystem.instance.timeQuantum == TimeQuantum.WeekHours)
        {
            DAY.SetActive(false);
            WEK.SetActive(true);
        }
        timeText.text = string.Format("{0}月{1}日     {2}", TimeEventSystem.instance.Month,
                                                                           TimeEventSystem.instance.Day,
                                                                           TimeEventSystem.instance.timeQuantum);
    }


}

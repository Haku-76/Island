using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timeText;
    private void OnEnable()
    {
        TimeEventSystem.instance.onTimeChange += UpDateTimeUI;
    }

    private void OnDisable()
    {
        TimeEventSystem.instance.onTimeChange -= UpDateTimeUI;
    }

    private void UpDateTimeUI(int Day,int Month,TimeQuantum timeQuantum)
    {
        //Debug.Log("1");
        timeText.text = string.Format("Month:{0} Day:{1} TimeQuantum:{2}", TimeEventSystem.Month,
                                                                           TimeEventSystem.Day,
                                                                           TimeEventSystem.timeQuantum);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

    public static LightManager Instance { get; private set; }
    private bool lightsOn = false;
    public GameObject environmentLight;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); 
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            environmentLight = GameObject.FindGameObjectWithTag("Light");
            if (environmentLight == null)
            {
                Debug.LogError("No GameObject with tag 'light' found in the scene.");
            }
        }
    }

    void OnEnable()
    {
        TimeEventSystem.onTimeChange += SwitchTime;
    }

    void OnDisable()
    {
        TimeEventSystem.onTimeChange -= SwitchTime;
    }

    void Start()
    {
        TurnLightsOff();
    }

    void Update()
    {
        // if(TimeEventSystem.instance.timeQuantum == TimeQuantum.WeekHours && !lightsOn)
        // {
        //     TurnLightsOn();
        // }
        // if(TimeEventSystem.instance.timeQuantum == TimeQuantum.DayTime && lightsOn)
        // {
        //     TurnLightsOff();
        // }
    }

    private void SwitchTime(int month, int day, TimeQuantum timeQuantum)
    {
        if(TimeEventSystem.instance.timeQuantum == TimeQuantum.WeekHours && !lightsOn)
        {
            TurnLightsOn();
        }
        if(TimeEventSystem.instance.timeQuantum == TimeQuantum.DayTime && lightsOn)
        {
            TurnLightsOff();
        }
    }

    public void TurnLightsOn()
    {
        lightsOn = true;
        environmentLight.SetActive(true);
    }

    public void TurnLightsOff()
    {
        lightsOn = false;
        environmentLight.SetActive(false);
    }
}

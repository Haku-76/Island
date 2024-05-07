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

    public void TurnLightsOn()
    {
        if (!lightsOn)
        {
            lightsOn = true;
            environmentLight.SetActive(true);
        }
    }

    public void TurnLightsOff()
    {
        if (lightsOn)
        {
            lightsOn = false;
            environmentLight.SetActive(false);
        }
    }
}

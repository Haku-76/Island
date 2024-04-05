using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
#region Singleton
    private static GameRoot _instance;

    public static GameRoot Instance{
        get => _instance;
    }
    void OnDestroy()
    {
        if(_instance == this)
            _instance = null;
    }
    #endregion
    public event Action EnterGameEvent;

    public event Action CloseGameEvent;

    public GameObject bar_Game;

    private GameObject barGame_instance;

    void OnEnable()
    {
        EnterGameEvent += OnEnterGameEvent;
        CloseGameEvent += OnCloseGameEvent;
    }

    void OnDisable()
    {
        EnterGameEvent -= OnEnterGameEvent;
        CloseGameEvent -= OnCloseGameEvent;
    }

    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    void OnEnterGameEvent()
    {
        barGame_instance = Instantiate(bar_Game, transform);
    }

    void OnCloseGameEvent()
    {
        if(barGame_instance != null)
        {
            Destroy(barGame_instance);
        }
    }


    public void EnterGame()
    {
        EnterGameEvent?.Invoke();
    }

    public void CloseGame()
    {
        CloseGameEvent?.Invoke();
    }


}
